using System;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.Cultures;
using VictorianAnimalGame.Engine.Province.Critters.Species;
using VictorianAnimalGame.Engine.Province.ProvinceData.DataGroupParts;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public record struct CritterDataGroup(SpeciesType Species, CultureType Culture, CritterClass Class)
{
    private CritterDataCounts _counts = new();
    private CritterDataSpecial _special = new();

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
    public uint Student => _special.Students;
    public uint Trained => _special.Trained;
    public uint Indisposed => _special.Indisposed;
    
    public (SpeciesType, CultureType, uint) GetTotal()
    {
        return (Species, Culture, _counts.Total);
    }
    
    public (SpeciesType, float) GetWorkforce()
    {
        float workforceModifier = Class switch
        {
            CritterClass.Lower => CritterDefines.Species[Species].WorkforceValue,
            CritterClass.Middle => CritterDefines.Species[Species].WorkforceValue,
            CritterClass.Upper => CritterDefines.Species[Species].WorkforceValue,
            _ => throw new ArgumentOutOfRangeException($"Unknown {nameof(CritterClass)}: {Class}")
        };
        float workforceTotal = MathF.Round((_counts.Workers - _special.Indisposed) * workforceModifier, 2);
        return (Species, workforceTotal);
    }

    public override string ToString()
    {
        return $"Data of {CritterDefines.Species[Species].SpeciesName}, {Culture}, {Class}: " +
               $"{_counts.ToString()}|{_special.ToString()}";
    }

    public override int GetHashCode() =>
        HashCode.Combine(Culture, Species, Class);

    public bool Equals(CritterDataGroup other) =>
        (Species, Culture, Class).Equals((other.Species, other.Culture, other.Class));
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