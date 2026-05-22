using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Regions;

public class RegionDetails
{
    public string Name { get; }
    public string Colour { get; }
    public FrozenSet<CantonId> CantonIds { get; }
    
    public RegionDetails(string newName, string newColour, HashSet<CantonId> newProvinces)
    {
        Name = newName;
        Colour = newColour;
        CantonIds = newProvinces.ToFrozenSet();
    }

    public override string ToString()
    {
        string regionString = $"Region(Name:{Name}|Colour:#{Colour}|Cantons:";
        foreach (CantonId id in CantonIds)
        {
            regionString += $"{id.Name}, ";
        }
        return regionString[..^2] + ")";
    }
}