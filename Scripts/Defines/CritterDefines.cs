using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.FileReader;
using VictorianAnimalGame.FileReader.DataConfig;
using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Defines;

public static class CritterDefines
{
    public static readonly FrozenDictionary<SpeciesType, Species> Species;
    public static readonly FrozenDictionary<string, SpeciesType> SpeciesTypes;

    static CritterDefines()
    {
        var (newSpeciesTypes, newSpecies) = CreateFrozenTypes();
        SpeciesTypes = newSpeciesTypes;
        Species = newSpecies;
    }
    
    private static (FrozenDictionary<string, SpeciesType>, FrozenDictionary<SpeciesType, Species>) CreateFrozenTypes()
    {
        List<SpeciesConfig> data = YamlReader.ReadFiles<SpeciesConfig>("Data/Species/");
        Dictionary<string, SpeciesType> speciesTypes = [];
        Dictionary<SpeciesType, Species> species = [];
        
        SetEmptySpecies(ref speciesTypes, ref species);

        for (ushort i = 1; i <= data.Count; i++)
        {
            var speciesData = data[i - 1].Species;
            
            SpeciesType newSpeciesType = new SpeciesType(i);
            speciesTypes.Add(speciesData.Name, newSpeciesType);

            Species newSpecies = new SpeciesBuilder()
                .SetSpeciesName(speciesData.Name)
                .SetSpeciesType(newSpeciesType)
                .SetAges(speciesData.Ages)
                .SetPeakFertility(speciesData.PeakFertility)
                .SetFoodConsumption(speciesData.FoodConsumption)
                .SetWorkforceValue(speciesData.WorkforceValue)
                .Build();
            species.Add(newSpeciesType, newSpecies);
            
            Console.WriteLine(newSpecies);
        }
        
        return (speciesTypes.ToFrozenDictionary(), species.ToFrozenDictionary());
    }

    private static void SetEmptySpecies(ref Dictionary<string, SpeciesType> speciesTypes, 
        ref Dictionary<SpeciesType, Species> species)
    {
        const string noneName = "None";
        SpeciesType noneType = new(0);
        Species noneSpecies = new SpeciesBuilder()
            .SetSpeciesName(noneName)
            .Build();
        
        speciesTypes.Add(noneName, noneType);
        species.Add(noneType, noneSpecies);
    }
}