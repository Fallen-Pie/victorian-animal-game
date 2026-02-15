using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using VictorianAnimalGame.Engine.Determinism;

namespace VictoriaAnimalGame.Tests.Determinism;

public class MortalitySystemTests
{
    private const int ArraySize = 128; // Multiple of 16 for SIMD
    private const ushort InitialPop = 1000;
    private const float SurvivalRate = 0.985f; // 98.5%

    [Fact]
    public void SameSeed_ProducesIdenticalResults()
    {
        // Arrange
        uint sharedSeed = 12345u;
        var pop1 = CreateSamplePopulation(InitialPop, ArraySize);
        var pop2 = CreateSamplePopulation(InitialPop, ArraySize);
        var curve = CreateSurvivalCurve(SurvivalRate, ArraySize);

        // Act
        ApplyMortalityTurbo(pop1, curve, sharedSeed);
        ApplyMortalityTurbo(pop2, curve, sharedSeed);

        // Assert
        // Every single age group must be identical on both runs
        Assert.Equal(pop1, pop2);
    }

    [Fact]
    public void DifferentSeeds_ProduceDifferentResults()
    {
        // Arrange
        var pop1 = CreateSamplePopulation(InitialPop, ArraySize);
        var pop2 = CreateSamplePopulation(InitialPop, ArraySize);
        var curve = CreateSurvivalCurve(SurvivalRate, ArraySize);

        // Act
        ApplyMortalityTurbo(pop1, curve, 11111u);
        ApplyMortalityTurbo(pop2, curve, 99999u);

        // Assert
        // With different seeds, the stochastic rounding "coin flips" will differ
        Assert.NotEqual(pop1, pop2);
    }

    [Theory]
    [InlineData(1000, 0.5f)] // 50% survival
    [InlineData(1000, 0.9f)] // 90% survival
    public void Mortality_StaysWithinStatisticalBounds(ushort popCount, float rate)
    {
        // Arrange
        int largeSize = 1000; // Larger sample for better averages
        var pop = CreateSamplePopulation(popCount, largeSize);
        var curve = CreateSurvivalCurve(rate, largeSize);
        double expectedTotal = popCount * rate * largeSize;

        // Act
        ApplyMortalityTurbo(pop, curve, 42u);

        // Assert
        double actualTotal = pop.Select(x => (int)x).Sum();
        double variance = Math.Abs(actualTotal - expectedTotal) / expectedTotal;

        // Stochastic rounding should keep us within 0.1% of the floating point target
        Assert.True(variance < 0.001, $"Variance {variance} was too high. Actual: {actualTotal}, Expected: {expectedTotal}");
    }

    // --- Helper Methods & Implementation ---

    private ushort[] CreateSamplePopulation(ushort val, int size) 
        => Enumerable.Repeat(val, size).ToArray();

    private ushort[] CreateSurvivalCurve(float rate, int size) 
        => Enumerable.Repeat((ushort)(rate * 65535f), size).ToArray();

    
    public static void ApplyMortalityTurbo(ushort[] population, ushort[] curve, uint newSeed)
    {
        // 1. Setup Safe References
        Span<ushort> popRef = population;
        Span<ushort> curveRef = curve;

        var rng = new VectorRng(newSeed);
        
        // Constants for RNG and Comparison
        // LCG Multiplier (Standard fast random constants)
        Vector256<ushort> vRngMult = Vector256.Create((ushort)25213);
        Vector256<ushort> vRngAdd = Vector256.Create((ushort)11);

        // Sign Flip Mask (0x8000) - Required for unsigned comparison in AVX2
        Vector256<ushort> vSignFlip = Vector256.Create((ushort)0x8000);

        int i = 0;
        int vecLen = Vector256<ushort>.Count; // 16

        if (Avx2.IsSupported && popRef.Length >= vecLen)
        {
            for (; i < popRef.Length; i += vecLen)
            {
                Vector256<uint> vRngState = rng.Next();
                Vector256<ushort> vRng = vRngState.AsUInt16();
                
                // 1. Load Data
                Vector256<ushort> vPop = Vector256.LoadUnsafe(ref popRef[i]);
                Vector256<ushort> vRate = Vector256.LoadUnsafe(ref curveRef[i]);

                // 2. Calculate Integer Part (Survivors)
                // (Pop * Rate) >> 16
                Vector256<ushort> vSurvivors = Avx2.MultiplyHigh(vPop, vRate);

                // 3. Calculate Fraction Part (The "Maybe" Survivor)
                // (Pop * Rate) & 0xFFFF - This is effectively the remainder
                Vector256<ushort> vFraction = Avx2.MultiplyLow(vPop, vRate);

                // 4. Generate Random Numbers (LCG Algorithm)
                // NextRng = (OldRng * 25213 + 11)
                vRng = Avx2.Add(Avx2.MultiplyLow(vRng, vRngMult), vRngAdd);

                // 5. Compare: Is Fraction > Random?
                // AVX2 Compare is SIGNED. 0xFFFF (-1) is less than 0x0000 (0).
                // Fix: XOR both with 0x8000 to flip the sign bit, making them behave like unsigned numbers.
                Vector256<short> vFracSigned = Avx2.Xor(vFraction, vSignFlip).AsInt16();
                Vector256<short> vRngSigned = Avx2.Xor(vRng, vSignFlip).AsInt16();

                // Result is 0xFFFF (-1) if True, 0x0000 (0) if False
                Vector256<short> vExtra = Avx2.CompareGreaterThan(vFracSigned, vRngSigned);

                // 6. Apply Result
                // Subtracting -1 (0xFFFF) is mathematically the same as Adding 1
                vSurvivors = Avx2.Subtract(vSurvivors, vExtra.AsUInt16());

                // 7. Store
                vSurvivors.StoreUnsafe(ref popRef[i]);
            }
        }
    }
}