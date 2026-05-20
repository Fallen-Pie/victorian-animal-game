namespace VictorianAnimalGame.FileReader.DataReader.DataConfig;

public class VegetationConfig : IDataConfig
{
    public VegetationData Vegetation { get; set; }
}

public class VegetationData
{
    public string Name { get; set; }
    
    public string Colour { get; set; }
}