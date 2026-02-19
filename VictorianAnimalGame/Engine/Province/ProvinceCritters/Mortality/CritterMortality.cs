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
            s += $"{deathChance / 65535f}|";
        }
        Console.WriteLine($"{s}");
    }
    
    public ushort[] GetMortalityCurve(float infantModifier, float diseaseSeverity, float industrialHazard)
    {
        Array.Clear(_mortalityCurve);
        Span<ushort> mortalityCurve = _mortalityCurve;

        float startMortality = 0.05f;
        int targetDeathAge = GetTargetDeathAge(0.8f);
        float invDuration = 1.0f / (targetDeathAge - Species.ElderAge);
        float mortalityGap = 1.0f - startMortality;
        float invWeeks = 1.0f / DateDefines.WeeksPerYear; // Using your 66-week year
        
        float infantSurvival = 1.0f - industrialHazard - diseaseSeverity; // Vulnerability is 1 here
        float deathRate = (1.0f - infantSurvival) * infantModifier;
        ushort infantWeeklyS = (ushort)((1f - (deathRate * invWeeks)) * 65535);
        // Slab 1: Infants (0 to _adolescentAge)
        mortalityCurve[..Species.AdolescentAge].Fill(infantWeeklyS);

        // Slab 2: Adults (_adolescentAge to _elderAge)
        float adultSurvival = 1.0f - industrialHazard - (diseaseSeverity * 0.5f);
        ushort adultWeeklyS = (ushort)((1f - ((1f - adultSurvival) * invWeeks)) * 65535);
        mortalityCurve[Species.AdolescentAge..Species.ElderAge].Fill(adultWeeklyS);
        
        // Slab 3: Elders (_elderAge to targetDeathAge)
        for (int age = Species.ElderAge; age <= targetDeathAge; age++)
        {
            float progress = (age - Species.ElderAge) * invDuration;
            float ease = progress * progress; // Faster than MathF.Pow(x, 2)
        
            float currentDeathRate = startMortality + (mortalityGap * ease);
            float survival = 1.0f - industrialHazard - diseaseSeverity - currentDeathRate;
        
            if (survival < 0) survival = 0;
            float sWeekly = 1f - ((1f - survival) * invWeeks);
            mortalityCurve[age] = (ushort)(sWeekly * 65535);
        }

        return _mortalityCurve;
        // Any remaining ages remain 0 from the Array.Clear
    }
    
    private int GetTargetDeathAge(float healthModifier)
    {
        int naturalDeathAge = Species.ElderAge + 2; // Temp, will need a Species.NaturalLifeSpan
        float gainFactor = 1.0f - MathF.Exp(-2.5f * healthModifier);
        naturalDeathAge += (int)((Species.MaxAge - naturalDeathAge) * gainFactor);
        if (naturalDeathAge >= Species.MaxAge) return Species.MaxAge - 1;
        return naturalDeathAge;
    }

    public override string ToString()
    {
        return $"Deaths: {_weeklyDependantsValue}/{_weeklyWorkersValue}/" +
               $"{_weeklyIncapacitatedValue}/{_weeklySoldiersValue}";
    }
}