using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Defines;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

namespace VictorianAnimalGame.Scripts.Map.Province;

public class LandProvince : IProvince
{
    public HashSet<CritterEntry> ProvinceCritters = [];
    public string ProvinceName;
    private readonly ProvinceDataFinder _provinceData = new();

    public void AddCritterEntry(CritterEntry newCritter) {
        if (!ProvinceCritters.Add(newCritter))
        {
            ProvinceCritters.TryGetValue(newCritter, out var item);
            item.GetCritterDetails().AddCritterCount(newCritter.GetCritterDetails().GetCritterCount());
        }
    }
        
    public string GetDetails()
    {
        string details = $"Info on this LandProvince\nHashCode: {GetHashCode()}\nCurrent Critters:\n";
        foreach (var critter in ProvinceCritters)
        {
            details += $"{critter}\n";
            if (CritterDefines.Species.TryGetValue(critter.GetCritterSpecies(), out var newData))
            {
                details += $"{newData.ToString()}\n";
            }
            
        }
        _provinceData.ChangeBehaviour(new ProvinceDataWorkforce());
        var i = _provinceData.RunBehaviour(ProvinceCritters);
        foreach (var critter in i)
        {
            details += $"{critter}\n";
        }
            
        _provinceData.ChangeBehaviour(new ProvinceDataSpeciesWorkforce());
        i = _provinceData.RunBehaviour(ProvinceCritters);
        foreach (var critter in i)
        {
            details += $"{critter}\n";
        }
        Console.WriteLine($"Size of {typeof(CritterEntry)} is {Marshal.SizeOf(typeof(CritterEntry))}");
        return details;
    }

    public void SetName()
    {
        ProvinceName = $"LandProvince-{GetHashCode()}";
    }
}