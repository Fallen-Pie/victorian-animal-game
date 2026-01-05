using System;
using System.Text;

namespace VictorianAnimalGame.Scripts.Goods.GoodTypes;

public readonly record struct GoodType
{
    public GoodType(string name, float price, float bulk)
    {
        Name = name;
        Price = price;
        Bulk = bulk;
        
        var value = (float)Math.Round(Price * 0.1f, 2);
        MinPrice = value < 0.01 ? 0.01f : value;
        MaxPrice = (float)Math.Round(Price * 10f, 2);
        PricePerBulk = (float)Math.Round(Price / Bulk, 2);
    }

    public string Name { get; init; }
    public float Price { get; init; }
    public float Bulk { get; init; }
    public float MinPrice { get; }
    public float MaxPrice { get;}
    public float PricePerBulk { get; }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append($"Name:{Name}|");
        builder.Append($"Price:{Price}|");
        builder.Append($"Bulk:{Bulk}|");
        builder.Append($"MinPrice:{MinPrice}|");
        builder.Append($"MaxPrice:{MaxPrice}|");
        builder.Append($"PricePerBulk:{PricePerBulk}");
        return builder.ToString();
    }
}