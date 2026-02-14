namespace VictorianAnimalGame.Engine.Determinism;

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

public struct VectorRng
{
    private Vector256<uint> _state;

    // Initialize with a single seed (e.g., ProvinceID + CurrentYear)
    public VectorRng(uint seed)
    {
        // We need 8 distinct seeds for the 8 integer lanes. 
        // We scramble the initial seed slightly for each lane so they don't produce identical patterns.
        uint[] seeds = new uint[8];
        for (int i = 0; i < 8; i++)
        {
            seeds[i] = seed + (uint)(i * 71932); // Simple offset scramble
            // Ensure non-zero seed
            if (seeds[i] == 0) seeds[i] = 0xDEADBEEF; 
        }
        _state = Vector256.Create(seeds);
    }

    // Advances the RNG state and returns the next 8 random uints
    // Algorithm: Xorshift32 (Very fast, excellent for games)
    public Vector256<uint> Next()
    {
        Vector256<uint> x = _state;
        
        // 1. x ^= x << 13
        x = Avx2.Xor(x, Avx2.ShiftLeftLogical(x, 13));
        
        // 2. x ^= x >> 17
        x = Avx2.Xor(x, Avx2.ShiftRightLogical(x, 17));
        
        // 3. x ^= x << 5
        x = Avx2.Xor(x, Avx2.ShiftLeftLogical(x, 5));

        _state = x; // Save state for next turn
        return x;
    }
}