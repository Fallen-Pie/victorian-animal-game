using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.FileReader;
using VictorianAnimalGame.FileReader.DataConfig;
using VictorianAnimalGame.Scripts.Goods.GoodTypes;

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
            
            var builder = new GoodTypeBuilder();
            GoodType newGood = builder.SetGoodName(goodData.Name)
                .SetGoodPrice(goodData.Price)
                .SetGoodBulk(goodData.Bulk)
                .Build();
            goodTypes.Add(goodData.Name, newGood);
            
            Console.WriteLine(newGood);
        }
        
        return goodTypes.ToFrozenDictionary();
    }
}