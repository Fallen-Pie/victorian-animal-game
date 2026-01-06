namespace VictorianAnimalGame.FileReader.DataConfig;

public class SpeciesConfig : IDataConfig
{
    public SpeciesData Species { get; set; }
}

public class SpeciesData
{
    public string Name { get; set; }
    
    public float FoodConsumption { get; set; }
    
    public float WorkforceValue { get; set; }

    public AgeStages Ages { get; set; }
    
    public byte PeakFertility { get; set; }
}

public class AgeStages
{
    public byte Adolescent { get; set; }
    public byte Adult { get; set; }
    public byte Elder { get; set; }
}
