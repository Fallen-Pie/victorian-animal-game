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
    
    public void IncrementTime()
    {
        if ((int)Phase < DateDefines.PhasesAmount - 1)
        {
            Phase += 1;
        }
        else if (Day < daysPerMonth[Month - 1])
        {
            Phase = 0;
            Day += 1;
        }
        else if (Month < DateDefines.MonthsAmount)
        {
            Phase = 0;
            Day = 1;
            Month += 1;
        }
        else
        {
            Phase = 0;
            Day = 1;
            Month = 1;
            Year += 1;
        }
    }
}