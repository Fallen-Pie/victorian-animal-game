using System.Collections.Generic;

namespace VictorianAnimalGame.FileReader.DataReader.DataConfig;

public class CantonConfig : IDataConfig
{
    public CantonData Canton { get; set; } 
}

public class CantonData
{
    public string Name { get; set; }
    
    public List<int> Provinces { get; set; }
    
    public string Colour { get; set; }
}