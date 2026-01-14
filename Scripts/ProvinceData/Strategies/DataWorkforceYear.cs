using System.Collections.Generic;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters;

namespace VictorianAnimalGame.Scripts.ProvinceData.Strategies;

public class DataWorkforceYear : IDataStrategy
{
    public HashSet<ProvinceCritterData> Execute(HashSet<CritterEntry> critters)
    {
        HashSet<ProvinceCritterData> workforce = [];
        foreach (CritterEntry critter in critters)
        {
            CritterDefines.Species.TryGetValue(critter.GetCritterSpecies(), out var value);
            //uint workAmountYear = (uint)(critter.GetCritterCount() * value.GetWorkforce());
            uint workAmountYear = critter.GetCritterTotalCount();
            ProvinceCritterData critterData = new(critter.GetCritterYear(), workAmountYear);
            if (workforce.TryGetValue(critterData, out var oldData))
            {
                oldData.AddAmount(workAmountYear);
            }
            else
            {
                workforce.Add(critterData);
            }
        }
        return workforce;
    }
}