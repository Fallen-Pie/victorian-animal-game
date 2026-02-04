using System;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Cultures;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public record struct CritterData(SpeciesType Species, CultureType Culture, CritterClass CritterClass)
{
    public int Dependants { get; set; }
    public int Workers { get; set; }
    public int Incapacitated { get; set; }
    public int Soldiers { get; set; }
    
    public int Literate { get; set; }
    public int Trained { get; set; }
    
    public (SpeciesType, float) GetWorkforce()
    {
        float workforceModifier = CritterClass switch
        {
            CritterClass.Commoners => Species.Workforce,
            CritterClass.Bourgeoisie => Species.Workforce,
            CritterClass.Elites => Species.Workforce,
            _ => throw new ArgumentOutOfRangeException($"Unknown {nameof(Critters.CritterClass)}: {CritterClass}")
        };
        float workforceTotal = MathF.Round(Workers * workforceModifier, 2);
        return (Species, workforceTotal);
    }

    public override string ToString()
    {
        return $"Data of {Species.Name}, {Culture}, {CritterClass}: " +
               $"{Dependants}.{Workers}.{Incapacitated}.{Soldiers}|" +
               $"{Literate}.{Trained}";
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