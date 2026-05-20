using VictorianAnimalGame.Engine.Components.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province.Critters.Mortality;
using VictorianAnimalGame.Engine.Randomness;

namespace VictoriaAnimalGame.Tests.Determinism;

public class MortalityRealityTests
{
    private const ushort StartingPop = 50_000; // Use a large number for precision
    private ushort[] _numbers = new ushort[new SpeciesType(2).MaxAge];

    [Theory]
    // Tests different scenarios: (InfantMod, Disease, Hazard, AgeToTest, ExpectedAnnualDeath)
    [InlineData(0.5f, 0.10f, 0.04f, 5,  0.07f)] // Infant: (0.1+0.04)*0.5 = 0.07
    [InlineData(1.0f, 0.20f, 0.05f, 12, 0.14f)] // Adult: 0.05 + (0.2*0.5) = 0.15
    public void WeeklyCurve_Matches_AnnualExpectedOutcome(
        float infantMod, float disease, float hazard, int ageToTest, float expectedAnnualDeath)
    {
        Array.Fill(_numbers, StartingPop);
        // 1. Arrange
        var system = new CritterMortality(new SpeciesType(2)); // Your class
        ushort[] curve = system.SetMortalityCurve(infantMod, disease, hazard);
        
        double expectedFinalPop = StartingPop * (1.0 - expectedAnnualDeath);

        // 2. Act: Simulate one full year (66 weeks) of survival
        for (int week = 0; week < DateDefines.WeeksPerYear; week++)
        {
            VectorRng rng = new(20);
            // Use double precision for the test to see how close the ushort gets
            _numbers.CalculateMortalitySimd(curve, rng);
        }

        // 3. Assert
        // We allow a small tolerance (0.5%) because the Linear Approximation 
        // is an estimate, not a perfect power curve.
        double tolerance = StartingPop * 0.005; 
        Assert.InRange(_numbers[ageToTest], expectedFinalPop - tolerance, expectedFinalPop + tolerance);
    }
}