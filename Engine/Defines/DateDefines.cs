using VictorianAnimalGame.Engine.Time;

namespace VictorianAnimalGame.Engine.Defines;

public static class DateDefines
{
    public const int PhasesAmount = 6;
    public const int WeeklyDaysAmount = 7;
    public const int MonthsAmount = 16;
    public const int DaysPerMonth = 30;
    
    private static readonly DateManager DateManager = new (DayPhase.Dawn, 1, DayOfWeek.Monday, 1, 1819);
    
    public static DayPhase Phase => DateManager.Phase;
    public static int Day => DateManager.Day;
    public static DayOfWeek WeekDay => DateManager.WeekDay;
    public static int Month => DateManager.Month;
    public static int Year => DateManager.Year;

    public static void IncrementTime()
    {
        DateManager.IncrementPhase();
    }

    public static string GetTime()
    {
        return DateManager.ToString();
    }
}