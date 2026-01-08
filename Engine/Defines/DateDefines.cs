using VictorianAnimalGame.Engine.Time;

namespace VictorianAnimalGame.Engine.Defines;

public static class DateDefines
{
    public const int PhasesPerDay = 6;
    public const int DaysPerWeek = 7;
    
    public const int DaysPerYear = 500;
    public const int MonthsPerYear = 16;
    public const int DaysPerMonth = DaysPerYear / MonthsPerYear;
    
    public static TimeOfDay Phase = TimeOfDay.Dawn;
    public static int Day = 1;
    public static int Month = 1;
    public static int Year = 1819;

    private static void IncreaseCurrentYear() => Year += 1;
    private static void IncreaseCurrentMonth() => Month += 1;
    private static void IncreaseCurrentDay() => Day += 1;
    private static void IncreaseCurrentPhase() => Phase += 1;

    public static string GetTime()
    {
        return $"{Phase}.{Day}.{Month}.{Year}";
    }
}