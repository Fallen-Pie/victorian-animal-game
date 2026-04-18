using System;

namespace VictorianAnimalGame.Engine.Province;

public readonly struct ProvinceType(ushort provinceId) : IEquatable<ProvinceType>
{
    private ushort ProvinceId { get; } = provinceId;
    
    public override bool Equals(object obj) => obj is ProvinceType other && Equals(other);
    public bool Equals(ProvinceType other) => ProvinceId == other.ProvinceId;
    public override int GetHashCode() => ProvinceId.GetHashCode();
    public static bool operator ==(ProvinceType left, ProvinceType right) => left.Equals(right);
    public static bool operator !=(ProvinceType left, ProvinceType right) => !(left == right);
    public override string ToString() => ProvinceId.ToString();
}