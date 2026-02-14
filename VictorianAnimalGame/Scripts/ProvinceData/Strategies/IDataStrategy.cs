using System.Collections.Generic;
using VictorianAnimalGame.Engine.Critters;

namespace VictorianAnimalGame.Scripts.ProvinceData.Strategies;

public interface IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters);
}