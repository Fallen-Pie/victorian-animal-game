namespace VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

public class GoodBuilder
{
    private string _goodName;
    private float _price;
    private float _bulk;
    
    public GoodBuilder SetGoodName(string name)
    {
        _goodName = name;
        return this;
    }
    
    public GoodBuilder SetGoodPrice(float price)
    {
        _price = price;
        return this;
    }
    
    public GoodBuilder SetGoodBulk(float bulk)
    {
        _bulk = bulk;
        return this;
    }
    
    public GoodDetails Build()
    {
        return new GoodDetails(_goodName, _price, _bulk);
    }
}