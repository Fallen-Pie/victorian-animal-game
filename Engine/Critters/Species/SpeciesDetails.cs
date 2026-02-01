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
        this.SpeciesType = SpeciesType;
        this.FoodConsumption = FoodConsumption;
        this.WorkforceValue = WorkforceValue;
        this.SpeciesName = SpeciesName;
        if (SpeciesName != "None")
        {
            FertilityByAge = GenerateFertilityCurve();
        }
    }

    //private readonly Consumption _speciesConsumption;

    private FrozenDictionary<int, float> GenerateFertilityCurve()
    {
        var results = new Dictionary<int, float>();

        int startX = AdultAge;
        int peakX = FertileAge;
        int endX = ElderAge;
        double steepness = 1.0;
        double maxHeight = 1.0;
        
        // Validation: Start < Peak < End
        if (startX >= peakX || peakX >= endX)
        {
            throw new ArgumentException("Constraints violated: Start must be < Peak and Peak must be < End.");
        }

        startX--;
        endX++;
        
        // Shift coordinates to relative space
        double L = endX - startX; // Relative End
        double P = peakX - startX; // Relative Peak
        double k = steepness;
        double n = (k * (L - P)) / P;

        // Normalization factor based on the relative peak
        double normalization = Math.Pow(P, k) * Math.Pow(L - P, n);

        for (int x = startX + 1; x < endX; x++)
        {
            // if (x == startX || x == endX)
            // {
            //     results[x] = 0.0;
            //     continue;
            // }

            // Calculate relative x
            double relX = x - startX;
            double raw = Math.Pow(relX, k) * Math.Pow(L - relX, n);
        
            results[x] = MathF.Round((float)((raw / normalization) * maxHeight), 3);
        }
        return results.ToFrozenDictionary();
    }

    public override string ToString()
    {
        return $"Name:{SpeciesName}|Food:{FoodConsumption}|Work:{WorkforceValue}|" +
               $"Age:{AdolescentAge}/{AdultAge}/{ElderAge}";
    }
}