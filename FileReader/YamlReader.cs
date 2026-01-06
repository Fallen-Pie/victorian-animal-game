using System.Collections.Generic;
using VictorianAnimalGame.FileReader.DataConfig;
using VictorianAnimalGame.FileReader.ReaderStrategies;

namespace VictorianAnimalGame.FileReader;

public static class YamlReader
{
    public const string FileExtension = "*.yaml";
    private static StrategyReader _strategyMethod;

    public static void ChangeBehaviour(StrategyReader newStrategy)
    {
        _strategyMethod = newStrategy;
    }
        
    public static List<IDataConfig> RunBehaviour(string fileLocation)
    {
        return _strategyMethod.ReadFiles(fileLocation);
    }
}