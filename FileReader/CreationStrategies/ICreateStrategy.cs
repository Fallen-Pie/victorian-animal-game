using System.Collections;
using System.Collections.Generic;
using VictorianAnimalGame.FileReader.DataConfig;

namespace VictorianAnimalGame.FileReader.CreationStrategies;

public interface ICreateStrategy<TConfig> where TConfig : IDataConfig
{
    public IDictionary Create(IEnumerable<TConfig> data);
}