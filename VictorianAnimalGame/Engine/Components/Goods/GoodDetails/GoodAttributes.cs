using System.Collections.Specialized;
using System.Text;

namespace VictorianAnimalGame.Engine.Components.Goods.GoodDetails;

public record struct GoodAttributes
{
    private uint attributeData;
    
    private const int TierShift = 0;
    private const int GoodShift = 3;
    private const int FactoryShift = 10;
    
    private const uint TierMask = 0x7;          // 3 bits (0-7)
    private const uint GoodsMask = 0x7F;        // 7 bits (0-127)
    private const uint FactoryMask = 0x3FFFFF;  // 22 bits (0-4,194,303)
    private const uint GoodsFactoryMask = 0xFFFFFFF8; // 29 bits
    
    public GoodAttributes(byte tier, byte goods, uint factoryId)
    {
        // Initialize with bit-packing logic
        attributeData = (tier & TierMask) |
                        ((goods & GoodsMask) << GoodShift) |
                        ((factoryId & FactoryMask) << FactoryShift);
    }
    
    //TODO: Make a way to get the tier for a specific good rather than a generic Tier
    // These should also return a specific Good 'Type' and a specific Tier based on the Good
    public byte GetTier() => (byte)(attributeData & TierMask);

    public byte GetGood() => (byte)((attributeData >> GoodShift) & GoodsMask);

    public uint GetFactoryId() => (attributeData >> FactoryShift) & FactoryMask;
    
    // Zero out the Tier bits to get the GoodFactoryID
    public uint GetGoodFactoryId() => attributeData & GoodsFactoryMask;

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.Append($"FactoryID: {GetFactoryId()}|");
        result.Append($"Good: {GetGood()}|");
        result.Append($"Good Tier: {GetTier()}");
        return result.ToString();
    }
}