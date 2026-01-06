using System;
using System.Collections;
using System.Collections.Generic;
using VictorianAnimalGame.FileReader.DataConfig;
using VictorianAnimalGame.Scripts.Goods.GoodTypes;

namespace VictorianAnimalGame.FileReader.CreationStrategies;

public class GoodsCreation : ICreateStrategy<GoodsConfig>
{
    public IDictionary Create(IEnumerable<GoodsConfig> data)
    {
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

        return goodTypes;
    }
}