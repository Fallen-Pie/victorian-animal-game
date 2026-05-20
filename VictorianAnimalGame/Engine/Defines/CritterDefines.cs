using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Components.Critters.Species;
using VictorianAnimalGame.FileReader.DataReader;
using VictorianAnimalGame.FileReader.DataReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class CritterDefines
{
    public static readonly FrozenDictionary<SpeciesType, SpeciesDetails> Species;
    public static readonly FrozenDictionary<string, SpeciesType> SpeciesTypes;

    static CritterDefines()
    {
        var (newSpeciesTypes, newSpecies) = CreateFrozenTypes();
        SpeciesTypes = newSpeciesTypes;
        Species = newSpecies;
    }
    
    private static (FrozenDictionary<string, SpeciesType>, FrozenDictionary<SpeciesType, SpeciesDetails>) CreateFrozenTypes()
    {
        List<SpeciesConfig> data = YamlReader.ReadFiles<SpeciesConfig>("Data/Species/");
        Dictionary<string, SpeciesType> speciesTypes = [];
        Dictionary<SpeciesType, SpeciesDetails> species = [];
        
        SetEmptySpecies(ref speciesTypes, ref species);

        for (ushort i = 1; i <= data.Count; i++)
        {
            var speciesData = data[i - 1].Species;
            
            SpeciesType newSpeciesType = new SpeciesType(i);
            speciesTypes.Add(speciesData.Name, newSpeciesType);

            SpeciesDetails newSpeciesDetails = new SpeciesBuilder()
                .SetSpeciesName(speciesData.Name)
                .SetSpeciesType(newSpeciesType)
                .SetAges(speciesData.Ages)
                .SetPeakFertility(speciesData.PeakFertility)
                .SetFoodConsumption(speciesData.FoodConsumption)
                .SetWorkforceValue(speciesData.WorkforceValue)
                .Build();
            species.Add(newSpeciesType, newSpeciesDetails);
            
            Console.WriteLine(newSpeciesDetails);
        }
        
        return (speciesTypes.ToFrozenDictionary(), species.ToFrozenDictionary());
    }

    private static void SetEmptySpecies(ref Dictionary<string, SpeciesType> speciesTypes, 
        ref Dictionary<SpeciesType, SpeciesDetails> species)
    {
        const string noneName = "None";
        SpeciesType noneType = new(0);
        SpeciesDetails noneSpeciesDetails = new SpeciesBuilder()
            .SetSpeciesName(noneName)
            .Build();
        
        speciesTypes.Add(noneName, noneType);
        species.Add(noneType, noneSpeciesDetails);
    }
    
    public static void Initialize() { }
}