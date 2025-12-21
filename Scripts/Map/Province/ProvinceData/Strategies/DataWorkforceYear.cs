using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Defines;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData.Strategies;

public class DataWorkforceYear : IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<ProvinceCritterData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            CritterDefines.Species.TryGetValue(critter.GetCritterSpecies(), out var value);
            uint stuff = (uint)(critter.GetCritterCount() * value.GetWorkforce());
            ProvinceCritterData critterData = new(critter.GetCritterYear(), stuff);
            if (!workforce.Add(critterData))
            {
                workforce.TryGetValue(critterData, out var oldData);
                oldData.AddAmount(stuff);
            }
        }
        return workforce;
    }
}