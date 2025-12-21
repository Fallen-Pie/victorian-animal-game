using System;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

[Flags]
public enum ProvinceDataFlags : ushort
{
    None = 0b_0000_0000,  // 0
    Culture = 0b_0000_0001,  // 1
    Year = 0b_0000_0010,  // 2
    Species = 0b_0000_0100,  // 4

    BaseCount = 0b_0001_0000,  // 16
    LiterateCount = 0b_0010_0000,  // 32
    TrainedCount = 0b_0100_0000,  // 64
    HappyCount = 0b_1000_0000,  // 128
    
    CultureYear = Culture | Year,
    YearSpecies = Species | Year,
    CultureSpecies = Culture | Species,
}