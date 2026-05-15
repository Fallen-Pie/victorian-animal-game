using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Map.Terrain.Typography;

public readonly struct TypographyType(byte typographyId) : IEquatable<TypographyType>
{
    private byte TypographyId { get; } = typographyId;
    
    public string Name => MapDefines.TerrainTypography[this].Name;
    public string Colour => MapDefines.TerrainTypography[this].Colour;
    
    public override bool Equals(object obj) => obj is TypographyType other && Equals(other);
    public bool Equals(TypographyType other) => TypographyId == other.TypographyId;
    public override int GetHashCode() => TypographyId.GetHashCode();
    public static bool operator ==(TypographyType left, TypographyType right) => left.Equals(right);
    public static bool operator !=(TypographyType left, TypographyType right) => !(left == right);
    public override string ToString() => TypographyId.ToString();
}