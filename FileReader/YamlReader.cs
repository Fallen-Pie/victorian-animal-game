using System.Collections;
using System.Collections.Generic;
using System.IO;
using VictorianAnimalGame.FileReader.CreationStrategies;
using VictorianAnimalGame.FileReader.DataConfig;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VictorianAnimalGame.FileReader;

public static class YamlReader
{
    private const string FileExtension = "*.yaml";
    private static ICreateStrategy<IDataConfig> _createStrategy;
    
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

    public static void ChangeStrategy<T>(ICreateStrategy<T> newStrategy) where T : IDataConfig
    {
        _createStrategy = newStrategy;
    }
    
    public static IDictionary Create(IEnumerable<IDataConfig> cd)
    {
        return _createStrategy.Create(cd);
    }
}