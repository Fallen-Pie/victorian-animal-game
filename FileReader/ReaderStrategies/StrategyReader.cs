using System.Collections.Generic;
using System.IO;
using VictorianAnimalGame.FileReader.DataConfig;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VictorianAnimalGame.FileReader.ReaderStrategies;

public abstract class StrategyReader
{
    private readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .Build();
    
    public abstract List<IDataConfig> ReadFiles(string fileName);

    protected IDataConfig DeserializerFile(string fileName)
    {
        using var yamlSpeciesInput = new StreamReader(fileName);
        return _deserializer.Deserialize<SpeciesConfig>(yamlSpeciesInput);
    }
}
