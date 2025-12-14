using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public interface IProvinceDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters);
}