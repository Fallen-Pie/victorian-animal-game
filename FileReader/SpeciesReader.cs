using System;
using System.IO;
using VictorianAnimalGame.Scripts.Critters.Species;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VictorianAnimalGame.FileReader;

public static class SpeciesReader
{
    public static void ReadSpeciesYaml(string filePath)
    {
        var yamlInput = new StreamReader(filePath);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        var data = deserializer.Deserialize<SpeciesConfig>(yamlInput);

        Console.WriteLine($"Species: {data.Species.Name}");
        Console.WriteLine($"Food: {data.Species.WorkforceValue}");
        Console.WriteLine($"Adult Age: {data.Species.Ages.Adult}");
    }
}

public class SpeciesConfig
{
    public SpeciesData Species { get; set; }
}

public class SpeciesData
{
    public string Name { get; set; }
    
    public double FoodConsumption { get; set; }
    
    public double WorkforceValue { get; set; }

    public AgeStages Ages { get; set; }
    
    public int PeakFertility { get; set; }
}

public class AgeStages
{
    public int Adolescent { get; set; }
    public int Adult { get; set; }
    public int Elder { get; set; }
}

