using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Extensions;

namespace VictorianAnimalGame.Engine.Province.ProvinceCritters;

public class CritterManager
{
    private readonly List<CritterData> _critterData = [];
    //private readonly CritterGrowth _critterGrowth;
    //private readonly CritterDecline _critterMortality;
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        foreach (CritterEntry critter in provincialCritterData)
        {
            CritterData newCritterData = new CritterData(critter);
            var Details = critter.Details;
            
            newCritterData.Dependants += Details.Dependants.SumArraySimd();
            newCritterData.Workers += Details.Workers.SumArraySimd();
            newCritterData.Soldiers += Details.Soldiers.SumArraySimd();
            newCritterData.Incapacitated += Details.Incapacitated.SumArraySimd();
            newCritterData.Literate += Details.Literate.SumArraySimd();
            newCritterData.Trained += Details.Trained.SumArraySimd();
            
            _critterData.Add(newCritterData);
        }
    }

    private int SumArray(ushort[] critterArray)
    {
        int critterCount = 0;
        ReadOnlySpan<ushort> critterSpan = critterArray;
        foreach (ushort critterAmount in critterSpan)
        {
            critterCount += critterAmount;
        }
        return critterCount;
    }

    public override string ToString()
    {
        string s = $"Province Manager|Length:{_critterData.Count}";
        foreach (CritterData critterData in _critterData)
        {
            s += $"\n{critterData}";
        }
        return s;
    }

    // public void GetWorkforceData()
    // {
    //     Dictionary<SpeciesType, float> workforceDictionary = [];
    //     Span<CritterData> critterDataSpan = CollectionsMarshal.AsSpan(_critterData);
    //     foreach (CritterData critterGroup in critterDataSpan)
    //     {
    //         (SpeciesType species, float workforceValue) = critterGroup.GetWorkforce();
    //         ref float existingWorkforce = 
    //             ref CollectionsMarshal.GetValueRefOrAddDefault(workforceDictionary, species, out bool _);
    //         existingWorkforce = MathF.Round(existingWorkforce + workforceValue, 2);
    //     }
    //     
    //     foreach (var workforce in workforceDictionary)
    //     {
    //         //Console.WriteLine($"{workforce.Key.Name} workforce: {workforce.Value}");;
    //     }
    // }

    


    
    
}