using System;
using System.Collections.Generic;
using System.IO;
using VictorianAnimalGame.FileReader.DataConfig;

namespace VictorianAnimalGame.FileReader.ReaderStrategies;

public class SpeciesStrategyReader : StrategyReader
{
    public override List<IDataConfig> ReadFiles(string fileLocation)
    {
        List<IDataConfig> speciesData = [];
        
        string[] speciesFiles = Directory.GetFiles(fileLocation, YamlReader.FileExtension);
        
        foreach (string file in speciesFiles)
        {
            IDataConfig newSpeciesData = (SpeciesConfig)DeserializerFile(file);
            speciesData.Add(newSpeciesData);
            
            var console = (SpeciesConfig)newSpeciesData;
            string debugString = $"Species: {console.Species.Name}|" +
                    $"Food: {console.Species.WorkforceValue}|" +
                    $"Adult Age: {console.Species.Ages.Adult}";
            Console.WriteLine(debugString);
        }

        return speciesData;
    }
}


