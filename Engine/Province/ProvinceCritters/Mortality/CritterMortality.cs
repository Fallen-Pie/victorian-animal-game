using System;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceCritters.Mortality;

public class CritterMortality(SpeciesType newSpecies)
{
    
}

// 1. The Stats Container (Immutable)
public record MortalityRates(
    double InfantMortality,      // Annual % chance (0.0 - 1.0)
    double AdultMortality,       // Annual % chance
    double DiseaseSusceptibility,// Multiplier for disease events (1.0 = normal)
    double SenescenceRate        // How fast they die after ElderAge
)
{
    // Helper to combine base rates with a modifier
    public MortalityRates Apply(MortalityModifier mod)
    {
        return new MortalityRates(
            InfantMortality * mod.InfantMult,
            AdultMortality * mod.AdultMult,
            DiseaseSusceptibility * mod.DiseaseMult,
            SenescenceRate * mod.SenescenceMult
        );
    }
}

// 2. The Modifier Definition (e.g., "Hospital", "Smog")
public record MortalityModifier(
    string Name,
    double InfantMult = 1.0,    // < 1.0 reduces death, > 1.0 increases it
    double AdultMult = 1.0,
    double DiseaseMult = 1.0,
    double SenescenceMult = 1.0
);

public record MortalityMultipliers(
    double Infant = 1.0, 
    double Adult = 1.0, 
    double Disease = 1.0, 
    double Senescence = 1.0
);

public abstract class ProvincialCondition
{
    public string Name { get; }
    
    // 0.0 (None) to 1.0 (Maximum Possible)
    public float Intensity { get; set; } 

    protected ProvincialCondition(string name, float initialIntensity)
    {
        Name = name;
        Intensity = Math.Clamp(initialIntensity, 0f, 1f);
    }

    // Each condition calculates its own multipliers based on Intensity
    public abstract MortalityMultipliers GetImpact();
}