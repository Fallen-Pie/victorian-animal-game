using System;
using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;

namespace VictorianAnimalGame.Engine.Province.ProvinceCritters.Growth;

public class CritterGrowth(SpeciesType Species)
{
    private double _weeklyValue;
    private int DailyBirths => (int)(_weeklyValue / DateDefines.WeeklyDaysAmount).ProbabilisticRound();

    public void CalculateWeeklyBirths(CritterDetails critterDetails, float birthRate = 2.5f)
    {
        _weeklyValue = 0;
        _weeklyValue += critterDetails.Dependants.CalculateFertilitySimd(Species);
        _weeklyValue += critterDetails.Workers.CalculateFertilitySimd(Species);
        _weeklyValue = _weeklyValue * birthRate / DateDefines.WeeksPerYear;
    }
    
    public override string ToString()
    {
        return $"Births: {DailyBirths}/{_weeklyValue.ProbabilisticRound()}/" +
               $"{(_weeklyValue * DateDefines.WeeksPerYear).ProbabilisticRound()}";
        //$"|Index:{_elderIndex}.{_adultIndex}.{_length}";
    }
}