using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public class ProvinceDataWorkforce : IProvinceDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<ProvinceCritterData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            ProvinceData.ProvinceCritterData critterData = new(critter.GetCritterOccupation(), critter.GetCritterCount());
            if (!workforce.Add(critterData))
            {
                workforce.TryGetValue(critterData, out var oldData);
                oldData.AddAmount(critter.GetCritterCount());
            }
        }
        return workforce;
    }
}