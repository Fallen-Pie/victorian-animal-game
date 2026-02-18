using System;
using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Randomness;

namespace VictorianAnimalGame.Engine.Province.ProvinceCritters.Mortality;

public class CritterMortality(SpeciesType Species)
{
    private int _weeklyDependantsValue;
    private int _weeklyWorkersValue;
    private int _weeklyIncapacitatedValue;
    private int _weeklySoldiersValue;

    private ushort[] _mortalityCurve = new ushort[Species.MaxAge + 16 & ~15];
    private float[] _unrefinedCurve = new float[Species.MaxAge + 16 & ~15];
    //private int DailyDeaths => (int)(_weeklyValue / DateDefines.WeeklyDaysAmount).ProbabilisticRound();
    
    public void CalculateWeeklyDeaths(CritterDetails critterDetails)
    {
        VectorRng rng = new(20);
        GetMortalityCurve(0.9f, 0.15f, 0.05f);
        
        _weeklyDependantsValue = critterDetails.Dependants.CalculateMortalitySimd(_mortalityCurve, rng);
        _weeklyWorkersValue = critterDetails.Workers.CalculateMortalitySimd(_mortalityCurve, rng);
        _weeklyIncapacitatedValue = critterDetails.Incapacitated.CalculateMortalitySimd(_mortalityCurve, rng);
        _weeklySoldiersValue = critterDetails.Soldiers.CalculateMortalitySimd(_mortalityCurve, rng);

        string s = "|";
        foreach (var deathChance in _mortalityCurve)
        {
            s += $"{deathChance}|";
        }
        Console.WriteLine($"{s}");
        s = "|";
        foreach (var deathChance in _unrefinedCurve)
        {
            s += $"{deathChance}|";
        }
        Console.WriteLine($"{s}");
    }
    
    public void GetMortalityCurve(float infantModifier, float diseaseSeverity, float industrialHazard)
    {
        Span<float> floatCurve = _unrefinedCurve;
        
        for (int age = 0; age <= Species.ElderAge; age++)
        {
            if (age >= Species.MaxAge)
            {
                floatCurve[age] = 0; // Hard cap
                continue;
            }
            float survivalChance = 1; 

            // --- A. Infant Mortality (Age A to B) ---
            if (age < Species.AdolescentAge)
            {
                float deathRate = 1 - survivalChance;
                deathRate *= infantModifier; 
                survivalChance = 1 - deathRate;
            }

            // --- B. Disease / Sanitation (The U-Shape) ---
            if (diseaseSeverity > 0)
            {
                float vulnerability = 1; 
                
                if (age > Species.AdolescentAge && age < Species.ElderAge) 
                    vulnerability = 0.2f; // Adults are strong
                // else if (age >= Species.ElderAge) 
                //     vulnerability = 1.5f; // Elders are weak
                //
                // Apply the disease penalty
                survivalChance -= (diseaseSeverity * vulnerability);
            }

            // --- C. Industrial Hazards (Flat) ---
            // Everyone loses a flat % chance of survival
            survivalChance -= industrialHazard;

            // --- Final Clamp and Store ---
            if (survivalChance < 0) survivalChance = 0;
            if (survivalChance > 1) survivalChance = 1;

            floatCurve[age] = survivalChance;

            if (age == Species.ElderAge)
            {
                ApplyDynamicElderMortality(floatCurve, 1, survivalChance * -1 + 1, 1.0f);
            }
        }
        BakeWeeklyCurve(floatCurve);
    }
    
    public void ApplyDynamicElderMortality(
        Span<float> floatCurve,
        float healthModifier,   // 0.0 = Primitive, 5.0+ = Advanced
        float startMortality,   // Mortality at C (e.g. 0.05 or 5%)
        float curveExponent     // 2.0 = Quadratic acceleration (Smooth ramp up)
    )
    {
        // --- Step 1: Calculate the "Target D" (Effective Max Age) ---

        int naturalDeathAge = Species.ElderAge + 2;
        // The maximum possible years we can add to the natural age
        int potentialGain = Species.MaxAge - naturalDeathAge; // Temp

        // Asymptotic calculation:
        // Health 0 -> Gain 0
        // Health High -> Gain approaches 25
        // Formula: Gain * (1 - e^(-health))
        // This creates a "Diminishing Returns" curve naturally.
        float gainFactor = 1.0f - MathF.Exp(-2.5f * healthModifier);
        
        int yearsGained = (int)(potentialGain * gainFactor);
        int targetDeathAge = naturalDeathAge + yearsGained;

        // Safety Clamp: Never actually hit the biological max
        if (targetDeathAge >= Species.MaxAge) targetDeathAge = Species.MaxAge - 1;

        // --- Step 2: Build the Curve from C to Target D ---

        int duration = targetDeathAge - Species.ElderAge;
        if (duration < 1) duration = 1; // Avoid divide by zero

        // The gap between "Start Mortality" (e.g. 5%) and "Total Death" (100%)
        float mortalityGap = 1.0f - startMortality;

        for (int age = Species.ElderAge; age <= floatCurve.Length - 1; age++)
        {
            // Case A: Beyond the target death age? Everyone dies.
            if (age >= targetDeathAge)
            {
                floatCurve[age] = 0; // 0% Survival
                continue;
            }

            // Case B: Inside the "Dying" zone (C to D)
            float progress = (float)(age - Species.ElderAge) / duration;

            // Apply the acceleration (Curve Exponent)
            // 1.0 = Straight Line, 2.0 = Slow start/Fast end
            float ease = MathF.Pow(progress, curveExponent);

            // Calculate current death rate
            float currentDeathRate = startMortality + (mortalityGap * ease);

            // Convert to Survival Rate (1.0 - Death)
            float survivalRate = 1.0f - currentDeathRate;

            // Store in Fixed Point (0 - 65535)
            floatCurve[age] = survivalRate;
        }
    }
    
    public void BakeWeeklyCurve(Span<float> floatCurve)
    {
        float oneOverWeekly = 1.0f / DateDefines.WeeksPerYear;
        Span<ushort> bakedCurve = _mortalityCurve;

        for (int year = 0; year < floatCurve.Length; year++)
        {
            // 1. Get the annual survival (e.g., 0.95)
            float sAnnual = floatCurve[year];

            // 2. Convert to weekly survival using the root
            // S_weekly = S_annual ^ (1/52)
            float sWeekly = MathF.Pow(sAnnual, oneOverWeekly);

            // 3. Store as fixed-point ushort (0 - 65535)
            bakedCurve[year] = (ushort)(sWeekly * 65535);
        }
    }

    public override string ToString()
    {
        return $"Deaths: {_weeklyDependantsValue}/{_weeklyWorkersValue}/" +
               $"{_weeklyIncapacitatedValue}/{_weeklySoldiersValue}";
    }
}