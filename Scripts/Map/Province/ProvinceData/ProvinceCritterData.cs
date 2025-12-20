using System;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public record ProvinceCritterData
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

    private ProvinceDataFlags _dataType { get; }
    private CritterCulture _culture { get; }
    private CritterOccupation _occupation { get; }
    private CritterSpecies _species { get; }
    private uint _amount { get; set; }
    
    public virtual bool Equals(ProvinceCritterData newCritter) =>
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