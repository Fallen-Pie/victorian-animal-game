using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Components.Critters;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Randomness;

namespace VictorianAnimalGame.Engine.Province.ProvinceCritters;

public class CritterManager
{
    private readonly List<CritterData> _critterData = [];
    private VectorRng _vProvinceRng = new(0);
    private ScalarRng _sProvinceRng = new(0);
    private Random _random = new();
 
    private Span<CritterData> DetailsSpan => CollectionsMarshal.AsSpan(_critterData);
    
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

    public void DailyProcessing()
    {
        _vProvinceRng.Reset((ushort)GetNewSeed());
        foreach (ref CritterData critterData in DetailsSpan)
        {
            critterData.DailyProcessing(_sProvinceRng);
        }
    }
    
    public void WeeklyProcessing()
    {
        _sProvinceRng.Reset(GetNewSeed());
        foreach (ref CritterData critterData in DetailsSpan)
        {
            critterData.WeeklyProcessing(_vProvinceRng);
        }
    }
    
    public void YearlyProcessing()
    {
        foreach (ref CritterData critterData in DetailsSpan)
        {
            critterData.YearlyProcessing();
        }
    }

    private uint GetNewSeed()
    {
        // TODO Make this deterministic, use of Random is just for testing so values arnt just flat
        return (uint)_random.Next(int.MaxValue);
    }

    public override string ToString()
    {
        string s = $"Province Manager|Length:{_critterData.Count}";
        foreach (CritterData critterData in _critterData)
        {
            s += $"\n\r{critterData}";
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