using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Goods.GoodTypes;
using VictorianAnimalGame.FileReader;
using VictorianAnimalGame.FileReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

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
        List<GoodsConfig> data = YamlReader.ReadFiles<GoodsConfig>("Data/Goods/");
        Dictionary<string, GoodType> goodTypes = [];
        
        foreach (GoodsConfig good in data)
        {
            var goodData = good.Goods;
            
            GoodType newGood = new GoodTypeBuilder()
                .SetGoodName(goodData.Name)
                .SetGoodPrice(goodData.Price)
                .SetGoodBulk(goodData.Bulk)
                .Build();
            goodTypes.Add(goodData.Name, newGood);
            
            Console.WriteLine(newGood.ToString());
        }
        
        return goodTypes.ToFrozenDictionary();
    }
}