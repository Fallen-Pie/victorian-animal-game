using System.Collections.Generic;
using VictorianAnimalGame.Engine.Components.Critters;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Scripts.ProvinceData.Strategies;

public class DataWorkforceSpecies : IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<ProvinceCritterData> workforce = [];
        // foreach (CritterEntry critter in critters)
        // {
        //     CritterDefines.Species.TryGetValue(critter.GetCritterSpecies(), out var workforceModifier);
        //     //uint workforceAmount = (uint)(critter.GetCritterCount() * workforceModifier.GetWorkforce());
        //     uint workforceAmount = critter.GetCritterTotalCount();
        //     ProvinceCritterData critterData = new(critter.GetCritterSpecies(), workforceAmount);
        //     if (workforce.TryGetValue(critterData, out var oldData))
        //     {
        //         oldData.AddAmount(workforceAmount);
        //     }
        //     else
        //     {
        //         workforce.Add(critterData);
        //     }
        // }
        return workforce;
    }
}