using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Map.Terrain;

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

public readonly record struct VegetationDetails(string newName, string newMapColour, VegetationType newType)
{
    public readonly string Name = newName;
    public readonly string Colour = newMapColour;
    
    private readonly VegetationType Type = newType;

    public override string ToString() => $"Vegetation(Name:{Name}|Colour:#{Colour}|Type:{Type})";
}