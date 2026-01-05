using System;

namespace VictorianAnimalGame.Scripts.Critters.Cultures;

public readonly struct CultureType(ushort cultureId) : IEquatable<CultureType>
{
    private ushort CultureId { get; } = cultureId;

    public override bool Equals(object obj) => obj is CultureType other && Equals(other);
    public bool Equals(CultureType other) => CultureId == other.CultureId;
    public override int GetHashCode() => CultureId.GetHashCode();
    public static bool operator ==(CultureType left, CultureType right) => left.Equals(right);
    public static bool operator !=(CultureType left, CultureType right) => !(left == right);
    public override string ToString() => CultureId.ToString();


}