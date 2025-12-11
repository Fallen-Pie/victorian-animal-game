using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public class ProvinceDataSpeciesWorkforce : IProvinceDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<ProvinceCritterData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            ProvinceCritterData critterData = new(critter.GetCritterOccupation(), 
                critter.GetCritterSpecies(), critter.GetCritterCount());
            if (!workforce.Add(critterData))
            {
                workforce.TryGetValue(critterData, out var oldData);
                oldData.AddAmount(critter.GetCritterCount());
            }
        }
        return workforce;
    }
}