using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Defines;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData.Strategies;

public class DataWorkforceSpecies : IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<ProvinceCritterData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            CritterDefines.Species.TryGetValue(critter.GetCritterSpecies(), out var workforceModifier);
            uint workforceAmount = (uint)(critter.GetCritterCount() * workforceModifier.GetWorkforce());
            ProvinceCritterData critterData = new(critter.GetCritterSpecies(), workforceAmount);
            if (!workforce.Add(critterData))
            {
                workforce.TryGetValue(critterData, out var oldData);
                oldData.AddAmount(workforceAmount);
            }
        }
        return workforce;
    }
}