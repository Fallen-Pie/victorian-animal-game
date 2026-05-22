using System.Collections.Generic;
using VictorianAnimalGame.Engine.Map.Cantons;

namespace VictorianAnimalGame.Engine.Map.Regions;

public class RegionBuilder
{
    private string _name;
    private string _colour;
    private HashSet<CantonId> _cantons;
    
    public RegionBuilder SetRegionName(string newName)
    {
        _name = newName;
        return this;
    }


    public RegionBuilder SetRegionColour(string newColour)
    {
        _colour = newColour;
        return this;
    }

    public RegionBuilder SetRegionCantons(List<string> newCantons, Dictionary<string, CantonId> cantonNames)
    {
        HashSet<CantonId> regionCantons = [];
        foreach (string cantonName in newCantons)
            regionCantons.Add(cantonNames[cantonName]);
        _cantons = regionCantons;
        return this;
    }

    public RegionDetails Build()
    {
        return new RegionDetails(_name, _colour, _cantons);
    }
}