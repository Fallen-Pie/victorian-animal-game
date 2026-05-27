using System;
using System.Collections.Frozen;
using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Types;

namespace VictorianAnimalGame.Engine.Province;

public readonly struct ProvinceId(ushort id) : IEquatable<ProvinceId>
{
    private ushort Id { get; } = id;

    private IProvince MappedProvince => MapDefines.ProvinceData[this];
    public string Name => MappedProvince.Name;
    public Color Colour => MappedProvince.MapColour;
    public IProvince Province => MappedProvince;
    public FrozenDictionary<ProvinceId, uint> Neighbours => MappedProvince.Neighbours;
    public Vector2I Centre => MappedProvince.Centre;
    
    public override bool Equals(object obj) => obj is ProvinceId other && Equals(other);
    public bool Equals(ProvinceId other) => Id == other.Id;
    public override int GetHashCode() => Id;
    public static bool operator ==(ProvinceId left, ProvinceId right) => left.Equals(right);
    public static bool operator !=(ProvinceId left, ProvinceId right) => !(left == right);
    public override string ToString() => Id.ToString();
}