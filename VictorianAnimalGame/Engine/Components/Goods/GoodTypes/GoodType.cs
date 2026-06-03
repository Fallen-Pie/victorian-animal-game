using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

public readonly struct GoodType(byte id) : IEquatable<GoodType>
{
    private byte Id { get; } = id;
    
    public string Name => GoodDefines.Goods[this].Name;
    public float Price => GoodDefines.Goods[this].Price;
    public float Bulk => GoodDefines.Goods[this].Bulk;
    public float MinPrice => GoodDefines.Goods[this].MinPrice;
    public float MaxPrice => GoodDefines.Goods[this].MaxPrice;
    public float PricePerBulk => GoodDefines.Goods[this].PricePerBulk;
    
    public uint Value => Id;
    
    public override bool Equals(object obj) => obj is GoodType other && Equals(other);
    public bool Equals(GoodType other) => Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(GoodType left, GoodType right) => left.Equals(right);
    public static bool operator !=(GoodType left, GoodType right) => !(left == right);
    public override string ToString() => Name;
}