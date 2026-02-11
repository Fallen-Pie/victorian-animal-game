using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private readonly List<CritterData> _critterData = [];
    //private readonly CritterGrowth _critterGrowth;
    //private readonly CritterDecline _critterMortality;
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        foreach (CritterEntry critter in provincialCritterData)
        {
            CritterData newCritterData = new CritterData(critter);
            newCritterData.Dependants += SumArray(critter.Details.Dependants);
            newCritterData.Workers += SumArray(critter.Details.Workers);
            newCritterData.Soldiers += SumArray(critter.Details.Soldiers);
            newCritterData.Incapacitated += SumArray(critter.Details.Incapacitated);
            newCritterData.Literate += SumArray(critter.Details.Literate);
            newCritterData.Trained += SumArray(critter.Details.Trained);
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