using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace VictorianAnimalGame.Engine.Critters.Species;

public readonly record struct SpeciesDetails
{
    public readonly string SpeciesName;
    
    public readonly int AdolescentAge;
    public readonly int AdultAge;
    public readonly int FertileAge;
    public readonly int ElderAge;
    public readonly int MaxAge;
    
    public readonly Range YoungRange;
    public readonly Range AdolescentRange;
    public readonly Range AdultAgeRange;
    public readonly Range ElderRange;
    
    public readonly SpeciesType SpeciesType;
    public readonly float FoodConsumption;
    public readonly float WorkforceValue;
    
    public readonly FrozenDictionary<int, float> FertilityByAge;

    public SpeciesDetails(SpeciesType SpeciesType,
        float FoodConsumption,
        float WorkforceValue,
        int AdolescentAge,
        int AdultAge,
        int FertileAge,
        int ElderAge,
        string SpeciesName)
    {
        this.AdolescentAge = AdolescentAge;
        this.AdultAge = AdultAge;
        this.FertileAge = FertileAge;
        this.ElderAge = ElderAge;
        this.MaxAge = (int)(ElderAge * 1.2);
        this.SpeciesType = SpeciesType;
        this.FoodConsumption = FoodConsumption;
        this.WorkforceValue = WorkforceValue;
        this.SpeciesName = SpeciesName;
        if (SpeciesName != "None")
        {
            FertilityByAge = GenerateFertilityCurve();
            YoungRange = ..AdolescentAge;
            AdolescentRange = AdolescentAge..AdultAge;
            AdultAgeRange = AdultAge..ElderAge;
            ElderRange = ElderAge..;
        }
    }

    //private readonly Consumption _speciesConsumption;

    private FrozenDictionary<int, float> GenerateFertilityCurve()
    {
        var rawValues = new Dictionary<int, float>();

        int startX = AdultAge;
        int peakX = FertileAge;
        int endX = ElderAge;
        float averageBirths = 4f;
        double steepness = 1.0;
    
        if (startX >= peakX || peakX >= endX)
        {
            throw new ArgumentException("Constraints violated: Start < Peak < End.");
        }

        startX--;
        endX++;
    
        double L = endX - startX; 
        double P = peakX - startX; 
        double k = steepness;
        double n = (k * (L - P)) / P;

        // 1. Calculate raw curve values (maxHeight is irrelevant here)
        double normalization = Math.Pow(P, k) * Math.Pow(L - P, n);
        double rawSum = 0;

        for (int x = startX + 1; x < endX; x++)
        {
            double relX = x - startX;
            double raw = (Math.Pow(relX, k) * Math.Pow(L - relX, n)) / normalization;
            rawValues[x] = (float)raw;
            rawSum += raw;
        }

        // 2. Calculate the scale factor to reach target sum
        // Factor = Target / CurrentSum
        float scaleFactor = averageBirths / (float)rawSum;

        // 3. Apply the scale factor and build the FrozenDictionary
        var results = new Dictionary<int, float>();
        float currentSum = 0;

        foreach (var kvp in rawValues)
        {
            float roundedValue = MathF.Round(kvp.Value * scaleFactor, 3);
            results[kvp.Key] = roundedValue;
            currentSum += roundedValue;
        }

        // --- VERIFICATION & ADJUSTMENT ---
        float difference = averageBirths - currentSum;
    
        // If the rounded sum doesn't match exactly, adjust the peak age
        // to absorb the rounding error (typically < 0.005)
        if (MathF.Abs(difference) > 0.0001f) 
        {
            results[FertileAge] = MathF.Round(results[FertileAge] + difference, 3);
        }

        return results.ToFrozenDictionary();
    }

    public override string ToString()
    {
        string result = $"Name:{SpeciesName}|Food:{FoodConsumption}|Work:{WorkforceValue}|" +
                        $"Age:{AdolescentAge}/{AdultAge}/{ElderAge}\r\n";
        foreach (var age in FertilityByAge)
        {
            result += $"{age.Key}:{age.Value}|";
        }
        return result;
    }
}