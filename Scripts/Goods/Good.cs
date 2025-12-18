namespace VictorianAnimalGame.Scripts.Goods;

public readonly record struct Good
{
    public Good(bool gathered, bool food, GoodType type, float price, float bulk)
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
    
    private float Price { get; }
    private float Bulk { get; }

    public float GetPrice()
    {
        return Price;
    }
    
    public float GetBulk()
    {
        return Bulk;
    }
}