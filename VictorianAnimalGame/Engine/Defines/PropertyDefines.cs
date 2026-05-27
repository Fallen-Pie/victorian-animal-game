using System.Collections.Concurrent;

namespace VictorianAnimalGame.Engine.Defines;

public class PropertyDefines
{
    // Tracks the current count for each distinct Category + SubCategory combination
    private static readonly ConcurrentDictionary<uint, uint> _sequences = new();

    public static uint GetNextSequence(uint prefix)
    {
        //TODO Add error checking for when values wrap
        // Might require a max value addition
        return _sequences.AddOrUpdate(prefix, 0, (key, currentVal) => currentVal + 1);
    }
}