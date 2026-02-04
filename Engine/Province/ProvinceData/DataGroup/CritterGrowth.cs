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
    }
    
    public CritterGrowth GetFertilityData(ReadOnlySpan<CritterDetails> critterSpan)
    {
        ReadOnlySpan<CritterDetails> fertileRange = critterSpan[_elderIndex.._adultIndex];
        FrozenDictionary<int, float> fertilityValues = _species.BirthsByAge;
        CritterGrowth critterGrowth = new CritterGrowth();
        foreach (var critter in fertileRange)
        {
            float yearlyValue = fertilityValues[DateDefines.Year - critter.Year];
            critterGrowth.BirthRate += (int)Math.Round(yearlyValue * (critter.Dependants));
            // if (critter.Occupied > critter.Dependants)
            // {
            //     critterBirthRate.BirthRate += (int)Math.Round(yearlyValue * (critter.Dependants));
            // }
            // else
            // {
            //     critterBirthRate.BirthRate += (int)Math.Round(yearlyValue * (critter.Occupied));
            // }

        }
        //Console.WriteLine($"{_species.Name} {critterBirthRate}");
        return critterGrowth;
    }
    
    public override string ToString()
    {
        return $"Current birth rate: {BirthRate}";
    }
}