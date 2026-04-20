using VictorianAnimalGame.Engine.Components.Critters;
using VictorianAnimalGame.Engine.Components.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Randomness;

namespace VictorianAnimalGame.Engine.Province.Critters.Growth;

public class CritterGrowth(SpeciesType Species)
{
    private float _weeklyValue;
    private float _dailyValue;

    public void DailyProcessing(CritterDetails critterDetails, ScalarRng birthRng)
    {
        critterDetails.AddBirths((ushort)(_dailyValue + birthRng.NextFloat()));
    }
    
    public void WeeklyProcessing(CritterDetails critterDetails) 
    {
        // TODO Have a birthrate modifier passed in
        SetWeeklyBirths(critterDetails);
    }

    private void SetWeeklyBirths(CritterDetails critterDetails, float birthRate = 7.5f)
    {
        _weeklyValue = 0;
        _weeklyValue += critterDetails.Dependants.CalculateFertilitySimd(Species);
        _weeklyValue += critterDetails.Workers.CalculateFertilitySimd(Species);
        _weeklyValue = (_weeklyValue * birthRate) / DateDefines.WeeksPerYear;
        _dailyValue = _weeklyValue / DateDefines.WeeklyDaysAmount;
    }
    
    public override string ToString()
    {
        return $"Births: {_dailyValue}/{_weeklyValue}/" +
               $"{_weeklyValue * DateDefines.WeeksPerYear}";
    }


}