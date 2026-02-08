using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private readonly List<CritterData> _critterData = [];
    //private readonly CritterGrowth _critterGrowth;
    //private readonly CritterDecline _critterDecline;
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        Console.WriteLine($"{provincialCritterData.Count}");
        foreach (CritterEntry critter in provincialCritterData)
        {
            CritterData newCritterData = new CritterData(critter);
            ReadOnlySpan<CritterDetails> critterSpan = CollectionsMarshal.AsSpan(critter.Details);
            //GetFertilityData(critter.Species, critterSpan);
            foreach (CritterDetails currentDetails in critterSpan)
            {
                newCritterData.Dependants += currentDetails.Dependants;
                newCritterData.Workers += currentDetails.Workers;
                newCritterData.Incapacitated += currentDetails.Incapacitated;
                newCritterData.Soldiers += currentDetails.Soldiers;
                newCritterData.Literate += currentDetails.Literate;
                newCritterData.Trained += currentDetails.Trained;
            }
            _critterData.Add(newCritterData);
        }
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