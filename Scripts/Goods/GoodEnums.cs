using System;

namespace VictorianAnimalGame.Scripts.Goods;

[Flags]
public enum GoodType
{
    None = 0b_0000_0000,
    ConsumerIndustry = 0b_0000_0001,
    LightIndustry = 0b_0000_0010,
    HeavyIndustry = 0b_0000_0100,
    Artisan = 0b_0000_1000,
    Agricultural = 0b_0001_0000,
    Forestry = 0b_0010_0000,
    Mining = 0b_0100_0000,
    Aquatic = 0b_1000_0000,
    Husbandry = 0b_0001_0000_0000,
    //Energy = 0b_0010_0000_0000,
    
    Food = Agricultural | Aquatic | Husbandry,
    Gathered = Forestry | Mining | Food,
    
    Industrial = LightIndustry | HeavyIndustry,
    Manufactured = ConsumerIndustry | Industrial | Artisan,
}

public enum GoodQuality
{
    Artisanal,
    Poor,
    Good,
    Excellent
}

