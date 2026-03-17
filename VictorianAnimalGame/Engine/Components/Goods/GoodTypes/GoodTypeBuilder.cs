namespace VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

public class GoodTypeBuilder
{
    private string _goodName;
    private float _price;
    private float _bulk;
    
    public GoodTypeBuilder SetGoodName(string name)
    {
        _goodName = name;
        return this;
    }
    
    public GoodTypeBuilder SetGoodPrice(float price)
    {
        _price = price;
        return this;
    }
    
    public GoodTypeBuilder SetGoodBulk(float bulk)
    {
        _bulk = bulk;
        return this;
    }
    
    public GoodType Build()
    {
        return new GoodType(_goodName, _price, _bulk);
    }
}