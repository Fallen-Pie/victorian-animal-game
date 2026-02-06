using System;
using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;

namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public record struct CritterGrowth
{
    public float BirthRate;
    private int _adultIndex;
    private int _elderIndex;
    private int _length;
    private SpeciesType _species;

    private void AdjustIndex(ReadOnlySpan<CritterDetails> critterSpan)
    {
        _adultIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - _species.AdultAge + 1);
        _elderIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - _species.ElderAge);
        _length = critterSpan.Length;
    }
    
    public void GetFertilityData(ReadOnlySpan<CritterDetails> critterSpan)
    {
        if (critterSpan.Length != _length)
        {
            AdjustIndex(critterSpan);
        }
        
        ReadOnlySpan<CritterDetails> fertileRange = critterSpan[_elderIndex.._adultIndex];
        FrozenDictionary<int, float> fertilityValues = _species.BirthsByAge;
        foreach (var critter in fertileRange)
        {
            float yearlyValue = fertilityValues[DateDefines.Year - critter.Year];
            BirthRate += (int)Math.Round(yearlyValue * (critter.Dependants));
            // if (critter.Occupied > critter.Dependants)
            // {
            //     critterBirthRate.BirthRate += (int)Math.Round(yearlyValue * (critter.Dependants));
            // }
            // else
            // {
            //     critterBirthRate.BirthRate += (int)Math.Round(yearlyValue * (critter.Occupied));
            // }
        }
    }
    
    public override string ToString()
    {
        return $"Current birth rate: {BirthRate}";
    }
}