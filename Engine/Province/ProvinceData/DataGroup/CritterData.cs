using System;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Cultures;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public record struct CritterData(SpeciesType Species, CultureType Culture, CritterClass CritterClass)
{
    private CritterDataCounts _counts = new();
    private CritterDataRates _rates = new();

    public uint Dependants
    {
        get => _counts.Dependants;
        set => _counts.Dependants = value;
    }
    public uint Workers
    {
        get => _counts.Workers;
        set => _counts.Workers = value;
    }
    public uint Incapacitated
    {
        get => _counts.Incapacitated;
        set => _counts.Incapacitated = value;
    }
    public uint Soldiers
    {
        get => _counts.Soldiers;
        set => _counts.Soldiers = value;
    }
    
    public uint Student => _rates.Students;
    public uint Trained => _rates.Trained;
    

    
    public (SpeciesType, float) GetWorkforce()
    {
        float workforceModifier = CritterClass switch
        {
            CritterClass.Commoners => Species.Workforce,
            CritterClass.Bourgeoisie => Species.Workforce,
            CritterClass.Elites => Species.Workforce,
            _ => throw new ArgumentOutOfRangeException($"Unknown {nameof(Critters.CritterClass)}: {CritterClass}")
        };
        float workforceTotal = MathF.Round(_counts.Workers * workforceModifier, 2);
        return (Species, workforceTotal);
    }

    public override string ToString()
    {
        return $"Data of {Species.Name}, {Culture}, {CritterClass}: " +
               $"{_counts.ToString()}|{_rates.ToString()}";
    }

    public override int GetHashCode() =>
        HashCode.Combine(Culture, Species, CritterClass);

    public bool Equals(CritterData other) =>
        (Species, Culture, Class: CritterClass).Equals((other.Species, other.Culture, other.CritterClass));
}

// public struct CritterDataConsumption
// {
//     Stuff for gathering the consumption of this group
// }

// public struct CritterDataNeeds
// {
//     Stuff for gathering supplied needs, wants and desires;
// }

// public struct CritterDataNeeds
// {
//     Stuff for doing budgeting;
// }