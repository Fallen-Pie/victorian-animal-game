using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Map.Terrain.Vegetation;

public readonly struct VegetationType(byte vegetationId) : IEquatable<VegetationType>
{
    private byte VegetationId { get; } = vegetationId;
    
    public string Name => MapDefines.TerrainVegetation[this].Name;
    public string Colour => MapDefines.TerrainVegetation[this].Colour;
    
    public override bool Equals(object obj) => obj is VegetationType other && Equals(other);
    public bool Equals(VegetationType other) => VegetationId == other.VegetationId;
    public override int GetHashCode() => VegetationId.GetHashCode();
    public static bool operator ==(VegetationType left, VegetationType right) => left.Equals(right);
    public static bool operator !=(VegetationType left, VegetationType right) => !(left == right);
    public override string ToString() => VegetationId.ToString();
}