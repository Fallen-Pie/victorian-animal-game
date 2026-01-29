using System;
using System.Numerics;
using VictorianAnimalGame.Engine.Critters;

namespace VictorianAnimalGame.Engine.Extensions;

public static class SpanExtensions
{
    public static int BinarySearchByYearValue(this ReadOnlySpan<CritterDetails> span, int targetValue)
    {
        int left = 0;
        int right = span.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            short midValue = span[mid].Year; // Direct access to the field without copy

            if (midValue == targetValue)
            {
                return mid; // Found the value
            }
            else if (midValue < targetValue)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        // Value not found. Returns the negative complement of the index where the 
        // target value would be inserted to maintain sort order.
        return ~left;
    }
}