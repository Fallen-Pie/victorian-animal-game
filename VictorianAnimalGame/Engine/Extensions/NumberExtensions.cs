using System;

namespace VictorianAnimalGame.Engine.Extensions;

public static class NumberExtensions
{
    public static double ProbabilisticRound(this double value)
    {
        double floor = Math.Floor(value);
        return Random.Shared.NextDouble() < (value - floor) ? floor + 1 : floor;
    }
}