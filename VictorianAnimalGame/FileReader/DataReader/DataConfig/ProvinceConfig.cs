namespace VictorianAnimalGame.FileReader.DataReader.DataConfig;

public class ProvinceConfig : IDataConfig
{
    public ProvinceData Province { get; set; } 
}

public class ProvinceData
{
    public ushort Id { get; set; }
    
    public string Type { get; set; }
    
    public string Name { get; set; }
    
    public string Colour { get; set; }
}