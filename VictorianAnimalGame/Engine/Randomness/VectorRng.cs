using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace VictorianAnimalGame.Engine.Randomness;

public struct VectorRng
{
    private const ushort Multiplier = 25213;
    private const ushort Increment = 11;
    private const ushort Scrambler = 31337;

    private Vector256<ushort> _state;
    private static readonly Vector256<ushort> vMultiplier = Vector256.Create(Multiplier);
    private static readonly Vector256<ushort> vIncrement = Vector256.Create(Increment);
    private static readonly Vector256<ushort> vScrambler = Vector256.Create(Scrambler);
    private static readonly Vector256<ushort> vIndex = Vector256<ushort>.Indices;

    // Initialize with a single seed (e.g., ProvinceID + CurrentYear)
    public VectorRng(ushort seed)
    {
        Reset(seed);
    }
    
    public void Reset(ushort seed)
    {
        Vector256<ushort> vScrambled = Avx2.MultiplyLow(vIndex, vScrambler);
        _state = Avx2.Xor(Vector256.Create(seed), vScrambled);
    }

    // Advances the RNG state and returns the next 16 random uints
    // Using MultiplyLow to maintain 16-bit truncation (modulo 65536)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector256<ushort> Next()
    {
        _state = Avx2.Add(Avx2.MultiplyLow(_state, vMultiplier), vIncrement);
        return _state;
    }


}