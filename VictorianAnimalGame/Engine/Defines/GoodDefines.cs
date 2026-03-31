using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Components.Goods.GoodTypes;
using VictorianAnimalGame.FileReader;
using VictorianAnimalGame.FileReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class GoodDefines
{
    public static readonly FrozenDictionary<GoodType, GoodDetails> Goods;
    public static readonly FrozenDictionary<string, GoodType> GoodTypes;

    static GoodDefines()
    {
        var (newGoodTypes, newGoods) = CreateFrozenGoods();
        GoodTypes = newGoodTypes;
        Goods = newGoods;
    }

    private static (FrozenDictionary<string, GoodType>, FrozenDictionary<GoodType, GoodDetails>) CreateFrozenGoods()
    {
        List<GoodsConfig> data = YamlReader.ReadFiles<GoodsConfig>("Data/Goods/");
        Dictionary<string, GoodType> goodTypes = [];
        Dictionary<GoodType, GoodDetails> goods = [];
        
        SetEmptyGoods(ref goodTypes, ref goods);

        for (byte i = 1; i <= data.Count; i++)
        {
            var goodData = data[i - 1].Goods;
            
            GoodType newGoodsType = new GoodType(i);
            goodTypes.Add(goodData.Name, newGoodsType);
            
            GoodDetails newGood = new GoodBuilder()
                .SetGoodName(goodData.Name)
                .SetGoodPrice(goodData.Price)
                .SetGoodBulk(goodData.Bulk)
                .Build();
            goods.Add(newGoodsType, newGood);
            
            Console.WriteLine(newGood.ToString());
        }
        
        return (goodTypes.ToFrozenDictionary(), goods.ToFrozenDictionary());
    }
    
    private static void SetEmptyGoods(ref Dictionary<string, GoodType> goodTypes, 
        ref Dictionary<GoodType, GoodDetails> goods)
    {
        const string noneName = "None";
        GoodType noneType = new(0);
        GoodDetails noneSpeciesDetails = new GoodBuilder()
            .SetGoodName(noneName)
            .Build();
        
        goodTypes.Add(noneName, noneType);
        goods.Add(noneType, noneSpeciesDetails);
    }
}