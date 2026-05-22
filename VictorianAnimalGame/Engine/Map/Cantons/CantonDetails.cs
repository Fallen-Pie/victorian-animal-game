using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Cantons;

public class CantonDetails
{
    public string Name { get; }
    public string Colour { get; }
    public FrozenSet<ProvinceId> ProvinceIds { get; }
    
    public CantonDetails(string newName, string newColour, HashSet<ProvinceId> newProvinces)
    {
        Name = newName;
        Colour = newColour;
        ProvinceIds = newProvinces.ToFrozenSet();
    }

    public override string ToString()
    {
        string cantonString = $"Canton(Name:{Name}|Colour:#{Colour}|Provinces:";
        foreach (ProvinceId id in ProvinceIds)
        {
            cantonString += $"{id.Name}, ";
        }
        return cantonString[..^2] + ")";
    }
}