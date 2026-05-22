using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Regions;

public readonly record struct RegionId(ushort regionId)
{
    private ushort Id { get; } = regionId;
    public string Name => AnnalsDefines.RegionData[this].Name;
    public string Colour => AnnalsDefines.RegionData[this].Colour;
    public FrozenSet<CantonId> CantonIds => AnnalsDefines.RegionData[this].CantonIds;

    public bool Equals(RegionId other) => Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
    public override string ToString() => Id.ToString();
}