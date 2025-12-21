using System;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public record ProvinceCritterData
{
    public ProvinceCritterData(short year, uint amount)
    {
        _dataType = ProvinceDataFlags.Occupation;
        _year = year;
        _amount = amount;
    }
    
    public ProvinceCritterData(CritterSpecies species, uint amount)
    {
        _dataType = ProvinceDataFlags.OccupationSpecies;
        _species = species;
        _amount = amount;
    }

    private ProvinceDataFlags _dataType { get; }
    private CritterCulture _culture { get; }
    private short _year { get; }
    private CritterSpecies _species { get; }
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