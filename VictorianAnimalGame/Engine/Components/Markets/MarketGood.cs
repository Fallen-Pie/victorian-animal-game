using VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

namespace VictorianAnimalGame.Engine.Components.Markets;

public struct MarketGood
{
    public GoodTrade TradeGood { get; }
    public uint Amount { get; set; }
    
    public MarketGood(GoodTrade good)
    {
        TradeGood = good;
        Amount = 0;
    }
    
    public MarketGood(GoodTrade good, uint amount)
    {
        TradeGood = good;
        Amount = amount;
    }
}