using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province.Critters;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private HashSet<CritterData> CritterData { get; set; }
    private HashSet<WorkforceData> WorkforceData { get; set; }

    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        CritterData = [];
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
                if (CritterData.TryGetValue(newCritterData, out var currentData))
                {
                    currentData.Count += critter.GetCritterClassCount(critterClass);
                    CritterData.Remove(newCritterData);
                    CritterData.Add(currentData);
                }
                else
                {
                    CritterData.Add(newCritterData);
                }
            }
        }

        foreach (CritterData critter in CritterData)
        {
            Console.WriteLine(critter.ToString());
        }
    }

    public void GetWorkforceData()
    {
        WorkforceData = [];
        Span<CritterData> critterDataSpan = [.. CritterData];
        foreach (CritterData critter in critterDataSpan)
        {
            if (critter.LifeStage == CritterLifeStage.Young) continue;
            WorkforceData newWorkforceData = new WorkforceData(critter.Species, critter.Class, critter.Count);
            if (WorkforceData.TryGetValue(newWorkforceData, out var currentData))
            {
                WorkforceData.Remove(newWorkforceData);
                currentData.AddWorkforce(newWorkforceData.WorkforceAmount);
                WorkforceData.Add(currentData);
            }
            else
            { 
                WorkforceData.Add(newWorkforceData);
            }
        }
        
        foreach (WorkforceData workforce in WorkforceData)
        {
            Console.WriteLine(workforce.ToString());
        }
    }
}