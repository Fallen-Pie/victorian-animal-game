using System;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public record struct ProvinceCritterData
{
    public ProvinceCritterData(CritterOccupation occupation, uint amount)
    {
        _dataType = ProvinceDataFlags.Occupation;
        _occupation = occupation;
        _amount = amount;
    }
    
    public ProvinceCritterData(CritterOccupation occupation, CritterSpecies species, uint amount)
    {
        _dataType = ProvinceDataFlags.OccupationSpecies;
        _occupation = occupation;
        _species = species;
        _amount = amount;
    }

    private readonly ProvinceDataFlags _dataType { get; }
    private readonly CritterCulture _culture { get; }
    private readonly CritterOccupation _occupation { get; }
    private readonly CritterSpecies _species { get; }
    private uint _amount { get; set; }
    
    public bool Equals(ProvinceCritterData newCritter) =>
        (_culture, _species, _occupation).Equals(
            (newCritter._culture, newCritter._species, newCritter._occupation));

    public override int GetHashCode() => 
        HashCode.Combine(_culture, _occupation, _species);

    public override string ToString()
    {
        return $"DataType:{_dataType}|Occupation:{_occupation}|Culture:{_culture}|Species:{_species}|Count:{_amount}";
    }
    
    public void AddAmount(uint newAmount)
    {
        _amount += newAmount;
    }
}

[Flags]
public enum ProvinceDataFlags : ushort
{
    None = 0b_0000_0000,  // 0
    Culture = 0b_0000_0001,  // 1
    Occupation = 0b_0000_0010,  // 2
    Species = 0b_0000_0100,  // 4

    BaseCount = 0b_0001_0000,  // 16
    LiterateCount = 0b_0010_0000,  // 32
    TrainedCount = 0b_0100_0000,  // 64
    HappyCount = 0b_1000_0000,  // 128
    
    CultureOccupation = Culture | Occupation,
    OccupationSpecies = Species | Occupation,
    CultureSpecies = Culture | Species,
}