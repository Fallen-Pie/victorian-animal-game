using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

public readonly struct GoodType(byte goodId) : IEquatable<GoodType>
{
    private byte GoodId { get; } = goodId;
    
    public string Name => GoodDefines.Goods[this].Name;
    public float Price => GoodDefines.Goods[this].Price;
    public float Bulk => GoodDefines.Goods[this].Bulk;
    public float MinPrice => GoodDefines.Goods[this].MinPrice;
    public float MaxPrice => GoodDefines.Goods[this].MaxPrice;
    public float PricePerBulk => GoodDefines.Goods[this].PricePerBulk;
    
    public uint RawValue => GoodId;
    
    public override bool Equals(object obj) => obj is GoodType other && Equals(other);
    public bool Equals(GoodType other) => GoodId == other.GoodId;
    public override int GetHashCode() => GoodId.GetHashCode();
    public static bool operator ==(GoodType left, GoodType right) => left.Equals(right);
    public static bool operator !=(GoodType left, GoodType right) => !(left == right);
    public override string ToString() => GoodId.ToString();
}