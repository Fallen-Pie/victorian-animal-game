using System;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Province.Critters;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private CritterData[] CritterData { get; set; }
    private WorkforceData[] WorkforceData { get; set; }
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        HashSet<CritterData> newCritterArray = [];
        Span<CritterEntry> critterEntrySpan = [.. provincialCritterData];
        foreach (CritterEntry critter in critterEntrySpan)
        {
            foreach (CritterClass critterClass in Enum.GetValues<CritterClass>())
            {
                CritterData newCritterData = new CritterData(
                    critter.GetCritterCulture(),
                    critter.GetCritterAge(),
                    critter.GetCritterSpecies(),
                    critterClass, 
                    critter.GetCritterClassCount(critterClass));
                if (newCritterArray.TryGetValue(newCritterData, out var currentData))
                {
                    currentData.Count += critter.GetCritterClassCount(critterClass);
                    newCritterArray.Remove(newCritterData);
                    newCritterArray.Add(currentData);
                }
                else
                {
                    newCritterArray.Add(newCritterData);
                }
            }
        }

        CritterData = [.. newCritterArray];

        foreach (CritterData critter in CritterData)
        {
            Console.WriteLine(critter.ToString());
        }
    }

    public void GetWorkforceData()
    {
        HashSet<WorkforceData> workforceDataHashSet = [];
        Span<CritterData> critterDataSpan = CritterData;
        foreach (CritterData critter in critterDataSpan)
        {
            if (critter.LifeStage == CritterLifeStage.Young) continue;
            WorkforceData newWorkforceData = new WorkforceData(critter.Species, critter.Class, critter.Count);
            if (workforceDataHashSet.TryGetValue(newWorkforceData, out var currentData))
            {
                workforceDataHashSet.Remove(newWorkforceData);
                currentData.AddWorkforce(newWorkforceData.WorkforceAmount);
                workforceDataHashSet.Add(currentData);
            }
            else
            { 
                workforceDataHashSet.Add(newWorkforceData);
            }
        }
        
        WorkforceData = [.. workforceDataHashSet];
        
        foreach (WorkforceData workforce in WorkforceData)
        {
            Console.WriteLine(workforce.ToString());
        }
    }
}