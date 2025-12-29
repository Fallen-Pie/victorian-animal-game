using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.FileReader;
using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Defines;

public static class CritterDefines
{
    public static readonly FrozenDictionary<string, SpeciesType> SpeciesTypes = CreateFrozenTypes();
    public static readonly FrozenDictionary<SpeciesType, string> SpeciesNames = CreateFrozenTypeNames();
    public static readonly FrozenDictionary<SpeciesType, Species> Species = CreateFrozenSpecies();

    private static FrozenDictionary<string, SpeciesType> CreateFrozenTypes()
    {
        SpeciesReader.ReadSpeciesYaml("Data/Species/Otter.yaml");
        Dictionary<string, SpeciesType> species = [];
        species.Add("None", new(0));
        species.Add("Otter", new(1));
        species.Add("Beaver", new(2));
        species.Add("Raccoon", new(3));
        return species.ToFrozenDictionary();
    }
    
    private static FrozenDictionary<SpeciesType, string> CreateFrozenTypeNames()
    {
        return SpeciesTypes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key).ToFrozenDictionary();
    }
    
    private static FrozenDictionary<SpeciesType, Species> CreateFrozenSpecies()
    {
        Dictionary<SpeciesType, Species> species = [];
        species.Add(SpeciesTypes["Otter"], new Species(SpeciesTypes["Otter"], 1, 1.1f,
            6, 10, 13, 22));
        species.Add(SpeciesTypes["Beaver"], new Species(SpeciesTypes["Otter"], 1, 0.9f,
            5, 10, 12, 15));
        species.Add(SpeciesTypes["Raccoon"], new Species(SpeciesTypes["Otter"], 1, 1, 
            5, 10, 12, 15));
        return species.ToFrozenDictionary();
    }
}

public static class MapDefines
{
    public static short Year = 1819;

    private static void IncreaseCurrentYear()
    {
        Year += 1;
    }
}