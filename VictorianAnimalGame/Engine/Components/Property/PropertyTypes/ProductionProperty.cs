using System;
using VictorianAnimalGame.Engine.Components.Goods.GoodTypes;
using VictorianAnimalGame.Engine.Components.Property.PropertyCategories;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyTypes;

public class ProductionProperty(GoodType good) : BaseProperty
{
    protected override PropertyId RawId { get; } = new(PropertyCode.Production, good);
    private readonly GoodType _goodDetails = good;

    public void RunProduction() 
    {
        Console.WriteLine($"Factory {RawId} is producing at 20 units/hr.");
    }
    
    public override string ToString() => $"{PropertyCode.Production}|Name: {Name}|Id: {RawId}";
}