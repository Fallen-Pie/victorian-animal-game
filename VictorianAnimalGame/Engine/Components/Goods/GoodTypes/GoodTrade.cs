using System.Text;
using VictorianAnimalGame.Engine.Components.Property.PropertyCategories;

namespace VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

public readonly record struct GoodTrade
{
    private readonly uint _tradeData;
    
    private const int TierShift = 0;
    private const int GoodShift = 3;
    private const int FactoryShift = 10;
    
    private const uint TierMask = 0x7;          // 3 bits (0-7)
    private const uint GoodsMask = 0x7F;        // 7 bits (0-127)
    private const uint FactoryMask = 0x3FFFFF;  // 22 bits (0-4,194,303)
    private const uint GoodsFactoryMask = 0xFFFFFFF8; // 29 bits
    
    public GoodTrade(byte tier, byte goods, uint factoryId)
    {
        // Initialize with bit-packing logic
        _tradeData = (tier & TierMask) |
                        ((goods & GoodsMask) << GoodShift) |
                        ((factoryId & FactoryMask) << FactoryShift);
    }
    
    //TODO: Make a way to get the tier for a specific good rather than a generic Tier
    // These should also return a specific Good 'Type' and a specific Tier based on the Good
    public byte GetTier() => (byte)(_tradeData & TierMask);
    public GoodType GetGood() => new((byte)((_tradeData >> GoodShift) & GoodsMask));
    public uint GetFactoryId() => (_tradeData >> FactoryShift) & FactoryMask;
    public PropertyId GetPropertyId() => new(_tradeData & GoodsFactoryMask);

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.Append($"FactoryID: {GetFactoryId()}|");
        result.Append($"Good: {GetGood()}|");
        result.Append($"Good Tier: {GetTier()}");
        return result.ToString();
    }
}