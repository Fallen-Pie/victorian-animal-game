using System;
using System.Collections.Frozen;
using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Types;

namespace VictorianAnimalGame.Engine.Province;

public readonly struct ProvinceId(ushort id) : IEquatable<ProvinceId>
{
    private ushort Id { get; } = id;
    
    public string Name => MapDefines.ProvinceData[this].Name;
    public IProvince Province => MapDefines.ProvinceData[this];
    public FrozenDictionary<ProvinceId, uint> Neighbours => MapDefines.ProvinceData[this].Neighbours;
    public Vector2I Centre => MapDefines.ProvinceData[this].Centre;
    
    public override bool Equals(object obj) => obj is ProvinceId other && Equals(other);
    public bool Equals(ProvinceId other) => Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(ProvinceId left, ProvinceId right) => left.Equals(right);
    public static bool operator !=(ProvinceId left, ProvinceId right) => !(left == right);
    public override string ToString() => Id.ToString();
}