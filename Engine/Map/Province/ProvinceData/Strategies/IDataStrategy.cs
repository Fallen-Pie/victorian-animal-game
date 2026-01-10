using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Engine.Map.Province.ProvinceData.Strategies;

public interface IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters);
}