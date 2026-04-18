using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Components.Critters;
using VictorianAnimalGame.Engine.Components.Critters.Species;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province.ProvinceCritters;
using VictorianAnimalGame.Scripts.ProvinceData;
using VictorianAnimalGame.Scripts.ProvinceData.Strategies;

namespace VictorianAnimalGame.Engine.Province;

public class LandProvince : IProvince
{
    public HashSet<CritterEntry> ProvinceCritters = [];
    public string ProvinceName;
    private readonly ProvinceDataFinder _provinceData = new();
    private readonly CritterManager _critterManager = new();
        
    public string GetDetails()
    {
        string details = $"Info on this LandProvince\nHashCode: {GetHashCode()}\nCurrent Critters:\n";
        foreach (var critter in ProvinceCritters)
        {
            details += $"{critter}\n";
        }

        ConsoleExtensions.GetByteSize<SpeciesType>();
        return details;
    }

    public void SetData()
    {
        _critterManager.GetCritterData(ProvinceCritters);
    }

    public string GetData()
    {
        string s = _critterManager.ToString();
        return s;
    }

    public void ProcessDaily()
    {
        _critterManager.DailyProcessing();
    }

    public void ProcessWeekly()
    {
        _critterManager.WeeklyProcessing();
    }
    
    public void ProcessYearly()
    {
        _critterManager.YearlyProcessing();
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