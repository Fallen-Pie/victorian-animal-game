using System;
using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public class CritterGrowth(SpeciesType newSpecies)
{
    private double _weeklyValue;
    private int DailyBirths => (int)(_weeklyValue / DateDefines.WeeklyDaysAmount).ProbabilisticRound();
    //public float EconomicData = 1;
    private int _adultIndex;
    private int _elderIndex;
    private int _length;

    public void CalculateWeeklyBirths(ReadOnlySpan<CritterDetails> critterSpan, float birthRate = 5f)
    {
        ReadOnlySpan<CritterDetails> detailsSpan = critterSpan;
        _weeklyValue = 0;
        
        if (detailsSpan.Length != _length)
        {
            _adultIndex = detailsSpan.BinarySearchByYearValue(DateDefines.Year - newSpecies.AdultAge + 1);
            _elderIndex = detailsSpan.BinarySearchByYearValue(DateDefines.Year - newSpecies.ElderAge);
            _length = detailsSpan.Length;
        }
        
        ReadOnlySpan<CritterDetails> fertileRange = detailsSpan[_elderIndex.._adultIndex];
        FrozenDictionary<int, float> fertilityValues = newSpecies.BirthDistribution;
        foreach (var critter in fertileRange)
        {
            float weeklyCurve = fertilityValues[DateDefines.Year - critter.Year] * birthRate / DateDefines.WeeksPerYear;
            _weeklyValue = weeklyCurve * (critter.Total / 2f);
            // if (critter.Occupied > critter.Dependants)
            // {
            //     critterBirthRate.BirthRate += (int)Math.Round(yearlyValue * (critter.Dependants));
            // }
            // else
            // {
            //     critterBirthRate.BirthRate += (int)Math.Round(yearlyValue * (critter.Occupied));
            // }
            //_weeklyValue = _weeklyValue.ProbabilisticRound();
        }
    }
    
    public override string ToString()
    {
        return $"Births: {DailyBirths}/{_weeklyValue.ProbabilisticRound()}/{(_weeklyValue * DateDefines.WeeksPerYear).ProbabilisticRound()}|Index:{_elderIndex}.{_adultIndex}.{_length}";
    }
}