using VictorianAnimalGame.Engine.Time;

namespace VictorianAnimalGame.Engine.Defines;

public static class DateDefines
{
    public const int PhasesAmount = 6;
    public const int WeeklyDaysAmount = 7;
    public const int MonthsAmount = 16;
    public const int DaysPerMonth = 30;
    
    //public const int DaysPerMonth = YearlyDaysAmount / MonthsAmount;

    private static readonly DateManager DateManager = new DateManager(DayPhase.Dawn, 1, 1, 1819);
    
    public static DayPhase Phase => DateManager.Phase;
    public static int Day => DateManager.Day;
    public static int Month => DateManager.Month;
    public static int Year => DateManager.Year;

    public static void IncrementTime()
    {
        DateManager.IncrementTime();
    }

    public static string GetTime()
    {
        return $"{Phase}.{Day}.{Month}.{Year}";
    }
}