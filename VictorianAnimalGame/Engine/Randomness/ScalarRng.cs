using System.Runtime.CompilerServices;

namespace VictorianAnimalGame.Engine.Randomness;

public struct ScalarRng
{
    private uint _state;

    // Standard LCG constants (Same as MSVC/DotNet Random)
    private const uint Multiplier = 214013;
    private const uint Increment = 2531011;
    private const uint Mixer = 0x9E3779B9;
    private const float Normalizer = 2.3283064365386963e-10f;

    public ScalarRng(uint seed)
    {
        // Mix the seed slightly to avoid "0" lock-ups
        _state = seed ^ Mixer; 
    }

    /// <summary>
    /// Returns a random uint [0..4,294,967,295]
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Next()
    {
        _state = _state * Multiplier + Increment;
        return _state;
    }

    /// <summary>
    /// Returns a random value between [0..max-1]
    /// Optimized using fixed-point math to avoid slow Modulo (%)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Next(uint max)
    {
        // This is a fast alternative to "Next() % max".
        // We multiply the random 32-bit number by the range, 
        // effectively treating the random number as a fraction 0.0 to 1.0.
        // Result = (Random * Range) / 2^32
        return (uint)((Next() * (ulong)max) >> 32);
    }

    /// <summary>
    /// Returns a float between 0.0 and 1.0
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float NextFloat()
    {
        // Multiply by 1.0 / 2^32
        return Next() * Normalizer;
    }
}