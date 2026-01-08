using System.Linq;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Time;

public class DateManager
{
    public DateManager(DayPhase startPhase, int startDay, int startMonth, int startYear)
    {
        Phase = startPhase;
        Day = startDay;
        Month = startMonth;
        Year = startYear;
        daysPerMonth = Enumerable.Repeat(DateDefines.DaysPerMonth, DateDefines.MonthsAmount).ToArray();
    }
    
    private readonly int[] daysPerMonth;
    
    public DayPhase Phase { get; private set; }
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    public void IncrementPhase()
    {
        if ((int)++Phase < DateDefines.PhasesAmount) return;
        Phase = 0;
        IncrementDay();
    }

    private void IncrementDay()
    {
        if (Day++ < daysPerMonth[Month - 1]) return;
        Day = 1;
        IncrementMonth();
    }
    
    private void IncrementMonth()
    {
        if (Month++ < DateDefines.MonthsAmount) return;
        Month = 1;
        IncrementYear();
    }
    
    private void IncrementYear()
    {
        Year++;
    }
}