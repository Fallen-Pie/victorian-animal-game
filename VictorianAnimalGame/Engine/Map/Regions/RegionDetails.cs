using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Regions;

public class RegionDetails
{
    public string Name { get; }
    public string Colour { get; }
    public FrozenSet<ProvinceId> ProvinceIds { get; }
    
    public RegionDetails(string newName, string newColour, HashSet<ProvinceId> newProvinces)
    {
        Name = newName;
        Colour = newColour;
        ProvinceIds = newProvinces.ToFrozenSet();
    }

    public override string ToString()
    {
        string regionString = $"Region(Name:{Name}|Colour:#{Colour}|Region Provinces:";
        foreach (ProvinceId id in ProvinceIds)
        {
            regionString += $"{id.Name}, ";
        }
        return regionString[..^2] + ")";
    }
}