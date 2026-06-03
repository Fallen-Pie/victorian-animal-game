using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

public readonly record struct GoodType(byte Id)
{
    private GoodDetails Good => GoodDefines.Goods[this];
    
    public string Name => Good.Name;
    public float Price => Good.Price;
    public float Bulk => Good.Bulk;
    public float MinPrice => Good.MinPrice;
    public float MaxPrice => Good.MaxPrice;
    public float PricePerBulk => Good.PricePerBulk;
    
    public uint Value => Id;
    
    public override string ToString() => Name;
}