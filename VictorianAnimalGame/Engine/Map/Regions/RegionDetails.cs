using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Regions;

public class RegionDetails
{
    public string Name { get; }
    public string Colour { get; }
    public ProvinceId[] ProvinceIds { get; }
    
    public RegionDetails(string newName, string newColour, List<ProvinceId> newProvinces)
    {
        Name = newName;
        Colour = newColour;
        ProvinceIds = newProvinces.ToArray();
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