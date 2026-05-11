using System;

namespace VictorianAnimalGame.Engine.Map.Terrain;

public readonly struct TypographyType(byte typographyId) : IEquatable<TypographyType>
{
    private ushort TypographyId { get; } = typographyId;
    
    
    
    public override bool Equals(object obj) => obj is TypographyType other && Equals(other);
    public bool Equals(TypographyType other) => TypographyId == other.TypographyId;
    public override int GetHashCode() => TypographyId.GetHashCode();
    public static bool operator ==(TypographyType left, TypographyType right) => left.Equals(right);
    public static bool operator !=(TypographyType left, TypographyType right) => !(left == right);
    public override string ToString() => typographyId.ToString();
}

public readonly record struct TypographyDetails(string newName, string newMapColour, TypographyType newType)
{
    private readonly string Name = newName;
    private readonly string Colour = newMapColour;
    private readonly TypographyType Type = newType;

    public override string ToString() => $"Typography(Name:{Name}|Colour:#{Colour}|Type:{Type})";
}