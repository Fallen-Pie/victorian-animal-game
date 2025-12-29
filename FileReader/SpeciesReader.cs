using System;
using System.IO;
using YamlDotNet.Serialization;

namespace VictorianAnimalGame.FileReader;

public static class SpeciesReader
{
    public static void ReadSpeciesYaml(string filePath)
    {
        var input = new StreamReader(filePath);

        var deserializer = new DeserializerBuilder().Build();
        
        var yamlObject = deserializer.Deserialize<dynamic>(input);
        
        Console.WriteLine($"{yamlObject["species"]["ages"]["adolescent"].ToString()}");
    }
}

