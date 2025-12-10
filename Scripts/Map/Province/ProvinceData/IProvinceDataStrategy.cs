using System;
using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData;
using VictorianAnimalGame.Scripts.Map.Provinces;

namespace VictorianAnimalGame.Scripts.Map.Province;

public interface IProvinceDataStrategy
{
    public HashSet<IProvinceData> Execute(HashSet<CritterEntry> critters);
}

public class ProvinceDataWorkforce : IProvinceDataStrategy
{
    public HashSet<IProvinceData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<IProvinceData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            WorkforceData newData = new(critter.GetCritterOccupation(), critter.GetCritterCount());
            if (!workforce.Add(newData))
            {
                workforce.TryGetValue(newData, out var oldData);
                oldData.AddAmount(critter.GetCritterCount());
            }
        }
        return workforce;
    }
}