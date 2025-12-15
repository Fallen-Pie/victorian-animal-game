namespace VictorianAnimalGame.Scripts.Goods;

public interface IGood
{
    protected bool Gathered { get; }
    protected bool Manufactured { get; }
    protected bool Food { get; }
    protected bool Consumer { get; }
    
    protected float BasePrice { get; }
    protected float MinPrice { get; }
    protected float MaxPrice { get; }
    protected float TransportCost { get; }
    
    
    public float GetBasePrice();
}