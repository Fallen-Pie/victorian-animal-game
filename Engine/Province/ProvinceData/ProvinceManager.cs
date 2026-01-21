using System;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private CritterDataGroup[] CritterData { get; set; }
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        HashSet<CritterDataGroup> newCritterArray = [];
        Span<CritterEntry> critterEntrySpan = [.. provincialCritterData];
        foreach (CritterEntry critter in critterEntrySpan)
        {
            foreach (CritterClass currentClass in Enum.GetValues<CritterClass>())
            {
                CritterDataGroup newData = new CritterDataGroup(
                    critter.Species, critter.Culture, currentClass);
                if (newCritterArray.TryGetValue(newData, out var updatedData))
                {
                    newCritterArray.Remove(newData);
                }
                else
                {
                    updatedData = newData;
                }

                switch (critter.LifeStage)
                {
                    case CritterLifeStage.Young:
                        updatedData.Dependants += critter.GetCritterClassCount(currentClass);
                        break;
                    default:
                        updatedData.Workers += critter.GetCritterClassCount(currentClass);
                        break;
                }
                newCritterArray.Add(updatedData);
            }
        }

        CritterData = [.. newCritterArray];

        foreach (CritterDataGroup critter in CritterData)
        {
            Console.WriteLine(critter.ToString());
        }
    }

    // public void GetWorkforceData()
    // {
    //     Dictionary<SpeciesType, float> workforceDataHashSet = [];
    //     Span<CritterData> critterDataSpan = CritterData;
    //     foreach (CritterData critter in critterDataSpan)
    //     {
    //         if (critter.LifeStage == CritterLifeStage.Young) continue;
    //         WorkforceData newWorkforceData = new WorkforceData(critter.Species, critter.Class, critter.Count);
    //         if (workforceDataHashSet.TryGetValue(newWorkforceData, out var currentData))
    //         {
    //             workforceDataHashSet.Remove(newWorkforceData);
    //             currentData.AddWorkforce(newWorkforceData.WorkforceAmount);
    //             workforceDataHashSet.Add(currentData);
    //         }
    //         else
    //         { 
    //             workforceDataHashSet.Add(newWorkforceData);
    //         }
    //     }
    //     
    //     WorkforceData = [.. workforceDataHashSet];
    //     
    //     foreach (WorkforceData workforce in WorkforceData)
    //     {
    //         Console.WriteLine(workforce.ToString());
    //     }
    // }
}