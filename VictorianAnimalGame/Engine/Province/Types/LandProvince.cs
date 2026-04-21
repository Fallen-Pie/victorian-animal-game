using System.Collections.Generic;
using VictorianAnimalGame.Engine.Components.Critters;
using VictorianAnimalGame.Engine.Components.Critters.Species;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province.Builder;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Scripts.ProvinceData;

namespace VictorianAnimalGame.Engine.Province.Types;

public class LandProvince(ProvinceId newId, uint newMapColour, string newName, ProvinceBuilderDetails details) : 
    IProvince(newId, newMapColour, newName, details)
{
    public HashSet<CritterEntry> ProvinceCritters = [];
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
        Name = $"LandProvince-{GetHashCode()}";
    }

    public void AddCritterToProvince(HashSet<CritterEntry> critterEntries)
    {
        ProvinceCritters.UnionWith(critterEntries);
    }
}