using System;

namespace VictorianAnimalGame.Scripts.Goods;

public readonly record struct Good
{
    public Good(bool gathered, bool food, GoodType type, Half price, Half bulk)
    {
        Food = food;
        Type = type;
        Price = price;
        Bulk = bulk;
        Gathered = gathered;
    }

    private bool Gathered { get; }
    private bool Food { get; }
    private GoodType Type { get; }
    
    private Half Price { get; }
    private Half Bulk { get; }

    public Half GetPrice()
    {
        return Price;
    }
    
    public Half GetBulk()
    {
        return Bulk;
    }
}