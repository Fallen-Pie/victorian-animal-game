using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VictorianAnimalGame.FileReader;

public static class SpeciesReader
{
    public static List<SpeciesConfig> ReadSpeciesYaml()
    {
        List<SpeciesConfig> speciesData = [];
        
        var speciesFiles = Directory.GetFiles("Data/Species/","*.yaml");
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        
        foreach (string file in speciesFiles)
        {
            var yamlSpeciesInput = new StreamReader(file);
            var newSpeciesData = deserializer.Deserialize<SpeciesConfig>(yamlSpeciesInput);
            speciesData.Add(newSpeciesData);
            Console.WriteLine($"Species: {newSpeciesData.Species.Name}");
            Console.WriteLine($"Food: {newSpeciesData.Species.WorkforceValue}");
            Console.WriteLine($"Adult Age: {newSpeciesData.Species.Ages.Adult}");
        }

        return speciesData;
    }
}

public class SpeciesConfig
{
    public SpeciesData Species { get; set; }
}

public class SpeciesData
{
    public string Name { get; set; }
    
    public float FoodConsumption { get; set; }
    
    public float WorkforceValue { get; set; }

    public AgeStages Ages { get; set; }
    
    public sbyte PeakFertility { get; set; }
}

public class AgeStages
{
    public sbyte Adolescent { get; set; }
    public sbyte Adult { get; set; }
    public sbyte Elder { get; set; }
}

