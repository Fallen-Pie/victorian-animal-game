using System;
using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;

namespace VictorianAnimalGame.Engine.Province.ProvinceData.Growth;

public class CritterGrowth(SpeciesType Species)
{
    private double _weeklyValue;
    private int DailyBirths => (int)(_weeklyValue / DateDefines.WeeklyDaysAmount).ProbabilisticRound();

    public void CalculateWeeklyBirths(CritterDetails critterDetails, float birthRate = 2f)
    {
        ReadOnlySpan<ushort> dependantsSpan = critterDetails.Dependants;
        ReadOnlySpan<ushort> workersSpan = critterDetails.Workers;
        _weeklyValue = 0;
        
        FrozenDictionary<int, float> fertilityValues = Species.BirthDistribution;
        foreach (var value in fertilityValues)
        {
            float weeklyCurve = value.Value * birthRate / DateDefines.WeeksPerYear;
            _weeklyValue += weeklyCurve * (dependantsSpan[value.Key] / 2f);
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
        return $"Births: {DailyBirths}/{_weeklyValue.ProbabilisticRound()}/" +
               $"{(_weeklyValue * DateDefines.WeeksPerYear).ProbabilisticRound()}";
        //$"|Index:{_elderIndex}.{_adultIndex}.{_length}";
    }
}