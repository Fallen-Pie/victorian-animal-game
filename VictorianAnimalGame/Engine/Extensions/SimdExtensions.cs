using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Determinism;

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

    public static float CalculateFertilitySimd(this ushort[] critterArray, SpeciesType Species)
    {
        if (critterArray == null || critterArray.Length == 0) return 0;

        Span<ushort> critterSpan = critterArray.AsSpan().Slice(Species.AdultAge, Species.BirthDistribution.Length);
        
        ulong totalProbabilityUnits = 0;
        int i = 0;

        // We process 8 ushorts at a time
        if (Vector256.IsHardwareAccelerated)
        {
            Vector256<uint> vAcc = Vector256<uint>.Zero;
            
            for (; i < critterSpan.Length; i += 16)
            {
                Vector256<ushort> vCountsRaw = Vector256.LoadUnsafe(ref critterSpan[i]);
                Vector256<ushort> vBirthsRaw = Vector256.LoadUnsafe(ref Species.BirthDistribution[i]);
                
                (Vector256<uint> popLow, Vector256<uint> popHigh) = Vector256.Widen(vCountsRaw);
                (Vector256<uint> rateLow, Vector256<uint> rateHigh) = Vector256.Widen(vBirthsRaw);
                
                Vector256<uint> prodLow = Vector256.Multiply(popLow, rateLow);
                Vector256<uint> prodHigh = Vector256.Multiply(popHigh, rateHigh);
                
                vAcc = Vector256.Add(vAcc, prodLow);
                vAcc = Vector256.Add(vAcc, prodHigh);
            }
            
            totalProbabilityUnits += Vector256.Sum(vAcc);
        }
        
        return totalProbabilityUnits / 65535f;
    }
    
    public static void ApplyMortalityTurbo(this ushort[] population, ushort[] curve, uint newSeed)
    {
        // 1. Setup Safe References
        Span<ushort> popRef = population;
        Span<ushort> curveRef = curve;

        var rng = new VectorRng(newSeed);
        
        // Constants for RNG and Comparison
        // LCG Multiplier (Standard fast random constants)
        Vector256<ushort> vRngMult = Vector256.Create((ushort)25213);
        Vector256<ushort> vRngAdd = Vector256.Create((ushort)11);

        // Sign Flip Mask (0x8000) - Required for unsigned comparison in AVX2
        Vector256<ushort> vSignFlip = Vector256.Create((ushort)0x8000);

        int i = 0;
        int vecLen = Vector256<ushort>.Count; // 16

        if (Avx2.IsSupported && popRef.Length >= vecLen)
        {
            for (; i < popRef.Length; i += vecLen)
            {
                Vector256<uint> vRngState = rng.Next();
                Vector256<ushort> vRng = vRngState.AsUInt16();
                
                // 1. Load Data
                Vector256<ushort> vPop = Vector256.LoadUnsafe(ref popRef[i]);
                Vector256<ushort> vRate = Vector256.LoadUnsafe(ref curveRef[i]);

                // 2. Calculate Integer Part (Survivors)
                // (Pop * Rate) >> 16
                Vector256<ushort> vSurvivors = Avx2.MultiplyHigh(vPop, vRate);

                // 3. Calculate Fraction Part (The "Maybe" Survivor)
                // (Pop * Rate) & 0xFFFF - This is effectively the remainder
                Vector256<ushort> vFraction = Avx2.MultiplyLow(vPop, vRate);

                // 4. Generate Random Numbers (LCG Algorithm)
                // NextRng = (OldRng * 25213 + 11)
                vRng = Avx2.Add(Avx2.MultiplyLow(vRng, vRngMult), vRngAdd);

                // 5. Compare: Is Fraction > Random?
                // AVX2 Compare is SIGNED. 0xFFFF (-1) is less than 0x0000 (0).
                // Fix: XOR both with 0x8000 to flip the sign bit, making them behave like unsigned numbers.
                Vector256<short> vFracSigned = Avx2.Xor(vFraction, vSignFlip).AsInt16();
                Vector256<short> vRngSigned = Avx2.Xor(vRng, vSignFlip).AsInt16();

                // Result is 0xFFFF (-1) if True, 0x0000 (0) if False
                Vector256<short> vExtra = Avx2.CompareGreaterThan(vFracSigned, vRngSigned);

                // 6. Apply Result
                // Subtracting -1 (0xFFFF) is mathematically the same as Adding 1
                vSurvivors = Avx2.Subtract(vSurvivors, vExtra.AsUInt16());

                // 7. Store
                vSurvivors.StoreUnsafe(ref popRef[i]);
            }
        }
    }
}