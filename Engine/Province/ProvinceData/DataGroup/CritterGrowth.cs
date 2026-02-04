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

    public CritterGrowth GetFertilityData(SpeciesType speciesType, ReadOnlySpan<CritterDetails> critterSpan)
    {
        int adultIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - speciesType.AdultAge + 1);
        int elderIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - speciesType.ElderAge);
        ReadOnlySpan<CritterDetails> fertileRange = critterSpan[elderIndex..adultIndex];
        FrozenDictionary<int, float> fertilityValues = GetFertilityByYear(speciesType);
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
        //Console.WriteLine($"{speciesType.Name} {critterBirthRate}");
        return critterGrowth;
    }
    
    public FrozenDictionary<int, float> GetFertilityByYear(SpeciesType speciesType)
    {
        FrozenDictionary<int, float> fertilityByYear = speciesType.BirthsByAge;
        // foreach (var d in fertilityByYear)
        // {
        //     Console.WriteLine($"Age:{d.Key}, Weight:{d.Value}");
        // }
        return fertilityByYear;
    }
    
    public override string ToString()
    {
        return $"Current birth rate: {BirthRate}";
    }
}