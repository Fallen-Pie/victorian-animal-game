using System;

namespace VictorianAnimalGame.Engine.Map.Regions;

public readonly record struct RegionId(ushort regionId)
{
    private ushort Id { get; } = regionId;
    
    public bool Equals(RegionId other) => Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
    public override string ToString() => Id.ToString();
}