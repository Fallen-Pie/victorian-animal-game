using System;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

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
        return $"DataType:{_dataType}|Year:{_year}|Culture:{_culture}|" +
               $"Species:{CritterDefines.Species[_species].SpeciesName}|Count:{_amount}";
    }
    
    public void AddAmount(uint newAmount)
    {
        _amount += newAmount;
    }
}