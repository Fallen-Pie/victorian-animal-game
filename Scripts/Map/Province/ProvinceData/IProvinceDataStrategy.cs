using System.Collections.Generic;
using System.ComponentModel;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

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

public class ProvinceDataSpeciesWorkforce : IProvinceDataStrategy
{
    public HashSet<IProvinceData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<IProvinceData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            WorkforceSpeciesData newData = new(critter.GetCritterOccupation(), 
                critter.GetCritterSpecies(), critter.GetCritterCount());
            if (!workforce.Add(newData))
            {
                workforce.TryGetValue(newData, out var oldData);
                oldData.AddAmount(critter.GetCritterCount());
            }
        }
        return workforce;
    }
}