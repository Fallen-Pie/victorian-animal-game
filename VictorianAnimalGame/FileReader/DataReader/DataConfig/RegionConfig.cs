using System.Collections.Generic;

namespace VictorianAnimalGame.FileReader.DataReader.DataConfig;

public class RegionConfig : IDataConfig
{
    public RegionData Region { get; set; } 
}

public class RegionData
{
    public string Name { get; set; }
    
    public List<string> Cantons { get; set; }
    
    public string Colour { get; set; }
}