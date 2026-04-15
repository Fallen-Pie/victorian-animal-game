using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VictorianAnimalGame.FileReader.DataReader;

public static class YamlReader
{
    private const string FileExtension = "*.yaml";
    
    public static List<TDataConfig> ReadFiles<TDataConfig>(string fileLocation)
    {
        List<TDataConfig> fileData = [];
        
        string[] speciesFiles = Directory.GetFiles(fileLocation, FileExtension);
        
        foreach (string file in speciesFiles)
        {
            TDataConfig newSpeciesData = DeserializerFile<TDataConfig>(file);
            fileData.Add(newSpeciesData);
        }

        return fileData;
    }
    
    private static TDataConfig DeserializerFile<TDataConfig>(string fileName)
    {
        IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        
        using var yamlSpeciesInput = new StreamReader(fileName);
        return deserializer.Deserialize<TDataConfig>(yamlSpeciesInput);
    }
}