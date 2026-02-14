using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using VictorianAnimalGame.Engine.Determinism;
using Xunit;

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
        ApplyMortalityDeterministic(pop1, curve, sharedSeed);
        ApplyMortalityDeterministic(pop2, curve, sharedSeed);

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
        ApplyMortalityDeterministic(pop1, curve, 11111u);
        ApplyMortalityDeterministic(pop2, curve, 99999u);

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
        ApplyMortalityDeterministic(pop, curve, 42u);

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

    public static void ApplyMortalityDeterministic(Span<ushort> population, ReadOnlySpan<ushort> survivalCurve, uint simulationSeed)
    {
        var rng = new VectorRng(simulationSeed);
        int i = 0;

        if (Avx2.IsSupported && population.Length >= 16)
        {
            unsafe
            {
                fixed (ushort* pPop = population, pCurve = survivalCurve)
                {
                    for (; i <= population.Length - 16; i += 16)
                    {
                        Vector256<ushort> vPop = Avx.LoadVector256(pPop + i);
                        Vector256<ushort> vRate = Avx.LoadVector256(pCurve + i);

                        (Vector256<uint> vPopL, Vector256<uint> vPopH) = Vector256.Widen(vPop);
                        (Vector256<uint> vRateL, Vector256<uint> vRateH) = Vector256.Widen(vRate);

                        Vector256<uint> vFullL = Vector256.Multiply(vPopL, vRateL);
                        Vector256<uint> vFullH = Vector256.Multiply(vPopH, vRateH);

                        Vector256<uint> vIntL = Vector256.ShiftRightLogical(vFullL, 16);
                        Vector256<uint> vFracL = Avx2.And(vFullL, Vector256.Create(0xFFFFu));
                    
                        Vector256<uint> vIntH = Vector256.ShiftRightLogical(vFullH, 16);
                        Vector256<uint> vFracH = Avx2.And(vFullH, Vector256.Create(0xFFFFu));

                        // 16 Deterministic Random Numbers
                        Vector256<uint> vRandL = Avx2.And(rng.Next(), Vector256.Create(0xFFFFu));
                        Vector256<uint> vRandH = Avx2.And(rng.Next(), Vector256.Create(0xFFFFu));

                        // Add 1 if Rand < Frac
                        vIntL = Vector256.Subtract(vIntL, Vector256.GreaterThan(vFracL, vRandL));
                        vIntH = Vector256.Subtract(vIntH, Vector256.GreaterThan(vFracH, vRandH));

                        Avx.Store(pPop + i, Vector256.Narrow(vIntL, vIntH));
                    }
                }
            }
        }

        // Fallback for remainder (Must use same RNG logic!)
        for (; i < population.Length; i++)
        {
            uint full = (uint)population[i] * survivalCurve[i];
            uint integerPart = full >> 16;
            uint fractionPart = full & 0xFFFF;
            
            // Consume 1 random number from the same RNG stream
            uint rand = rng.Next().GetElement(0) & 0xFFFF; 
            
            population[i] = (ushort)(integerPart + (fractionPart > rand ? 1u : 0u));
        }
    }
}