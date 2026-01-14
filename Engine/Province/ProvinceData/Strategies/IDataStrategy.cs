using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province.Critters;

namespace VictorianAnimalGame.Engine.Province.ProvinceData.Strategies;

public interface IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters);
}