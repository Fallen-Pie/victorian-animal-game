using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Extensions;

public static class SimdExtensions
{
    public static double ApplyMortalitySimd(this Span<ushort> ageSlice, float weeklyDeathProb)
    {
        // 1. Create a vector filled with the death probability
        //float survivalRate = 1.0f - weeklyDeathProb;
        Vector256<float> vProb = Vector256.Create(weeklyDeathProb);
        double count = 0f;
        
        int i = 0;
        // Process in chunks of 8 (Size of Vector256<float>)
        for (; i <= ageSlice.Length; i += 8)
        {
            Vector128<ushort> raw = Vector128.Create(ageSlice);
            // Load 8 age buckets into a vector
            // We convert to float because our probability math is floating point
            var (left, right) = Vector128.Widen(raw);
            Vector256<uint> vInts = Vector256.Create(left, right);
            
            Vector256<float> vCounts = Vector256.ConvertToSingle(vInts);
    
            // Calculate Expected Deaths: Counts * Probability
            Vector256<float> vExpectedDeaths = Vector256.Multiply(vCounts, vProb);
    
            // Subtract deaths from counts
            count += Vector256.Sum(vExpectedDeaths);
    
            // Convert back to int and store
            // Note: This effectively 'Floors' the value. 
            // We handle the fractional 'Probabilistic' part below for precision.
            // Vector256<int> vFinal = Vector256.ConvertToInt32(vNewCounts);
            // vFinal.StoreUnsafe(ref ageSlice[i]);
        }
    
        // 2. Clean up remainder (if the slice length isn't a multiple of 8)
        // for (; i < ageSlice.Length; i++)
        // {
        //     float expected = ageSlice[i] * weeklyDeathProb;
        //     ageSlice[i] -= (int)Math.Floor(expected + (Random.Shared.NextDouble() < (expected % 1) ? 1 : 0));
        // }
        return count;
    }
    
    public static int SumArraySimd(this ushort[] critterArray)
    {
        if (critterArray == null || critterArray.Length == 0) return 0;

        Span<ushort> span = critterArray;
        uint totalSum = 0;
        int i = 0;

        // We process 8 ushorts at a time
        if (Vector256.IsHardwareAccelerated)
        {
            Vector256<uint> vAcc = Vector256<uint>.Zero; // Our 32-bit accumulator

            for (; i < span.Length; i += 16)
            {
                // 1. Load 8 ushorts: [u1, u2, u3, u4, u5, u6, u7, u8]
                Vector256<ushort> vRaw = Vector256.LoadUnsafe(ref span[i]);
                
                // WidenLower/Upper converts 16-bit ushorts to 32-bit uints
                // (Wait! Vector256 holds 16 ushorts, so Widen converts 8 at a time)
                vAcc = Vector256.Add(vAcc, Vector256.WidenLower(vRaw));
                vAcc = Vector256.Add(vAcc, Vector256.WidenUpper(vRaw));
            }

            // 4. "Horizontal Sum": Add the 4 integers inside the vector together
            totalSum = Vector256.Sum(vAcc);
        }
        
        for (; i < span.Length; i++)
        {
            totalSum += span[i];
        }

        return (int)totalSum;
    }
}