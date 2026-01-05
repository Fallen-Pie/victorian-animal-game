using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Goods.GoodTypes;

namespace VictorianAnimalGame.Scripts.Defines;

public static class GoodDefines
{
    public static readonly FrozenDictionary<string, GoodType> GoodTypes;

    static GoodDefines()
    {
        var goodTypes = CreateFrozenGoods();
        GoodTypes = goodTypes;
    }

    private static FrozenDictionary<string, GoodType> CreateFrozenGoods()
    {
        Dictionary<string, GoodType> goodTypes = [];
        
        var builder = new GoodTypeBuilder();
        GoodType newGood = builder.SetGoodName("Wood")
            .SetGoodPrice(2.5f)
            .SetGoodBulk(3f)
            .Build();
        goodTypes.Add("Wood", newGood);
        
        newGood = builder.SetGoodName("Fish")
            .SetGoodPrice(1.5f)
            .SetGoodBulk(1f)
            .Build();
        goodTypes.Add("Fish", newGood);
        
        newGood = builder.SetGoodName("Radio")
            .SetGoodPrice(8f)
            .SetGoodBulk(1.5f)
            .Build();
        goodTypes.Add("Radio", newGood);
        
        return goodTypes.ToFrozenDictionary();
    }
}