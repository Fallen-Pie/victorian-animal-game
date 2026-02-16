using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using VictorianAnimalGame.Engine.Extensions;

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
        ushort sharedSeed = 12345;
        var pop1 = CreateSamplePopulation(InitialPop, ArraySize);
        var pop2 = CreateSamplePopulation(InitialPop, ArraySize);
        var curve = CreateSurvivalCurve(SurvivalRate, ArraySize);

        // Act
        pop1.ApplyMortalityTurbo(curve, sharedSeed);
        pop2.ApplyMortalityTurbo(curve, sharedSeed);

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
        pop1.ApplyMortalityTurbo(curve, 11111);
        pop2.ApplyMortalityTurbo(curve, 22222);

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
        pop.ApplyMortalityTurbo(curve, 42);

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
}