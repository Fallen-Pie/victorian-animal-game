using System;
using System.Linq;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Time;

public class DateManager
{
    public DateManager(DayPhase startPhase, int startDay, DayOfWeek startDayOfWeek, 
        int startMonth, int startYear)
    {
        Phase = startPhase;
        Day = startDay;
        WeekDay = startDayOfWeek;
        Month = startMonth;
        Year = startYear;
        _daysPerMonth = Enumerable.Repeat(DateDefines.DaysPerMonth, DateDefines.MonthsAmount).ToArray();
    }
    
    private readonly int[] _daysPerMonth;
    private int _currentMonth => Month - 1;
    
    public DayPhase Phase { get; private set; }
    public int Day { get; private set; }
    public DayOfWeek WeekDay { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    public void IncrementPhase()
    {
        if ((int)++Phase < Enum.GetNames<DayPhase>().Length) return;
        Phase = 0;
        IncrementDay();
        IncrementWeek();
    }

    private void IncrementDay()
    {
        if (Day++ < _daysPerMonth[_currentMonth]) return;
        Day = 1;
        IncrementMonth();
    }
    
    private void IncrementWeek()
    {
        if ((int)++WeekDay < Enum.GetNames<DayOfWeek>().Length) return;
        WeekDay = 0;
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

    public override string ToString()
    {
        return $"{Phase} on {WeekDay} the {FormatDay(Day)}, {FormatMonth(Month)}, {Year}";
    }

    private static string FormatDay(int day)
    {
        return (day % 10) switch
        {
            1 => $"{day}st",
            2 => $"{day}nd",
            3 => $"{day}rd",
            _ => $"{day}th"
        };
    }
    
    private static string FormatMonth(int month)
    {
        return $"MonthName({month})";
    }
}