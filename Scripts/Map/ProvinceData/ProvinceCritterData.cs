using System;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Critters.Species;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

namespace VictorianAnimalGame.Scripts.Map.ProvinceData;

public record ProvinceCritterData
{
    public ProvinceCritterData(short year, uint amount)
    {
        _dataType = ProvinceDataFlags.Year;
        _year = year;
        _amount = amount;
    }
    
    public ProvinceCritterData(SpeciesType species, uint amount)
    {
        _dataType = ProvinceDataFlags.Species;
        _species = species;
        _amount = amount;
    }

    private ProvinceDataFlags _dataType { get; }
    private CritterCulture _culture { get; }
    private short _year { get; }
    private SpeciesType _species { get; }
    private uint _amount { get; set; }
    
    public virtual bool Equals(ProvinceCritterData newCritter) =>
        (_culture, _species, _year).Equals(
            (newCritter._culture, newCritter._species, newCritter._year));

    public override int GetHashCode() => 
        HashCode.Combine(_culture, _year, _species);

    public override string ToString()
    {
        return $"DataType:{_dataType}|Year:{_year}|Culture:{_culture}|Species:{_species}|Count:{_amount}";
    }
    
    public void AddAmount(uint newAmount)
    {
        _amount += newAmount;
    }
}