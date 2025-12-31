using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.FileReader;
using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Defines;

public static class CritterDefines
{
    //private FileReader fileReader = new()
    
    public static readonly FrozenDictionary<string, SpeciesType> SpeciesTypes;
    public static readonly FrozenDictionary<SpeciesType, string> SpeciesNames;
    public static readonly FrozenDictionary<SpeciesType, Species> Species;

    static CritterDefines()
    {
        var (newSpeciesTypes, newSpecies) = CreateFrozenTypes();
        SpeciesTypes = newSpeciesTypes;
        Species = newSpecies;
        SpeciesNames = CreateFrozenTypeNames();
    }
    
    private static (FrozenDictionary<string, SpeciesType>, FrozenDictionary<SpeciesType, Species>) CreateFrozenTypes()
    {
        List<SpeciesConfig> data = SpeciesReader.ReadSpeciesYaml();
        Dictionary<string, SpeciesType> speciesTypes = [];
        Dictionary<SpeciesType, Species> species = [];
        
        speciesTypes.Add("None", new(0));
        
        for (ushort i = 1; i <= data.Count; i++)
        {
            var speciesData = data[i - 1].Species;
            
            SpeciesType newSpeciesType = new SpeciesType(i);
            speciesTypes.Add(speciesData.Name, newSpeciesType);

            Species newSpecies = new SpeciesBuilder()
                .SetSpeciesType(newSpeciesType)
                .SetAges(speciesData.Ages)
                .SetPeakFertility(speciesData.PeakFertility)
                .SetFoodConsumption(speciesData.FoodConsumption)
                .SetWorkforceValue(speciesData.WorkforceValue)
                .Build();
            species.Add(newSpeciesType, newSpecies);
        }
        
        return (speciesTypes.ToFrozenDictionary(), species.ToFrozenDictionary());
    }
    
    private static FrozenDictionary<SpeciesType, string> CreateFrozenTypeNames()
    {
        return SpeciesTypes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key).ToFrozenDictionary();
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