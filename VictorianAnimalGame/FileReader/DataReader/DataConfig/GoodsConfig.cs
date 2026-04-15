namespace VictorianAnimalGame.FileReader.DataReader.DataConfig;

public class GoodsConfig : IDataConfig
{
    public GoodsData Goods { get; set; }
}

public class GoodsData
{
    public string Name { get; set; }
    
    public float Price { get; set; }
    
    public float Bulk { get; set; }
}