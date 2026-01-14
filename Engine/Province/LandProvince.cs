using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.ProvinceData;
using VictorianAnimalGame.Scripts.ProvinceData;
using VictorianAnimalGame.Scripts.ProvinceData.Strategies;

namespace VictorianAnimalGame.Engine.Province;

public class LandProvince : IProvince
{
    public HashSet<CritterEntry> ProvinceCritters = [];
    public string ProvinceName;
    private readonly ProvinceDataFinder _provinceData = new();
    private readonly ProvinceManager _provinceManager = new();

    // public void AddCritterEntry(CritterEntry newCritter) {
    //     if (!ProvinceCritters.Add(newCritter))
    //     {
    //         ProvinceCritters.TryGetValue(newCritter, out var item);
    //         item.GetCritterDetails().AddCritterCount(newCritter.GetCritterDetails().GetCritterCount());
    //     }
    // }
        
    public string GetDetails()
    {
        string details = $"Info on this LandProvince\nHashCode: {GetHashCode()}\nCurrent Critters:\n";
        foreach (var critter in ProvinceCritters)
        {
            details += $"{critter}\n";
            // if (CritterDefines.Species.TryGetValue(critter.GetCritterSpecies(), out var newData))
            // {
            //     details += $"{newData.ToString()}\n";
            // }
            
        }
        _provinceData.ChangeBehaviour(new DataWorkforceYear());
        var i = _provinceData.RunBehaviour(ProvinceCritters);
        foreach (var critter in i)
        {
            details += $"{critter}\n";
        }
            
        _provinceData.ChangeBehaviour(new DataWorkforceSpecies());
        i = _provinceData.RunBehaviour(ProvinceCritters);
        foreach (var critter in i)
        {
            details += $"{critter}\n";
        }
        Console.WriteLine($"Size of {typeof(CritterEntry)} is {Marshal.SizeOf<CritterEntry>()}");
        Console.WriteLine($"Size of {typeof(CritterData)} is {Marshal.SizeOf<CritterData>()}");
        _provinceManager.GetData(ProvinceCritters);
        return details;
    }

    public void SetName()
    {
        ProvinceName = $"LandProvince-{GetHashCode()}";
    }

    public void AddCritterToProvince(HashSet<CritterEntry> critterEntries)
    {
        ProvinceCritters.UnionWith(critterEntries);
    }
}