using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Map.Cantons;

namespace VictorianAnimalGame.Engine.Map.Regions;

public readonly record struct RegionId(ushort Id)
{
    private RegionDetails CurrentRegion => AnnalsDefines.RegionData[this];
    
    public string Name => CurrentRegion.Name;
    public string Colour => CurrentRegion.Colour;
    public FrozenSet<CantonId> CantonIds => CurrentRegion.CantonIds;
    
    //public override string ToString() => Id.ToString();
}