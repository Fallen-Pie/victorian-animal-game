namespace VictorianAnimalGame.Scripts.Goods;

public interface IGood
{
    protected bool Consumed { get; }
    protected bool Food { get; }
    protected GoodType Type { get; }
    
    
    protected float BasePrice { get; }
    protected float MinPrice { get; }
    protected float MaxPrice { get; }
    protected float TransportCost { get; }
    
    public float GetBasePrice();
}