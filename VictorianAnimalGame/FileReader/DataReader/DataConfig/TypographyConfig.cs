namespace VictorianAnimalGame.FileReader.DataReader.DataConfig;

public class TypographyConfig : IDataConfig
{
    public TypographyData Typography { get; set; }
}

public class TypographyData
{
    public string Name { get; set; }
    
    public string Colour { get; set; }
}