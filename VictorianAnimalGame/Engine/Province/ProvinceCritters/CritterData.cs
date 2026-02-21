using System;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Cultures;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province.ProvinceCritters.Growth;
using VictorianAnimalGame.Engine.Province.ProvinceCritters.Mortality;
using VictorianAnimalGame.Engine.Randomness;

namespace VictorianAnimalGame.Engine.Province.ProvinceCritters;

public record struct CritterData(CritterEntry _critterDetail)
{
    private readonly CritterEntry _critterDetail = _critterDetail;
    private SpeciesType Species => _critterDetail.Species;
    private CultureType Culture => _critterDetail.Culture;
    private CritterClass Class => _critterDetail.Class;
    private CritterDetails Details => _critterDetail.Details;
    
    private readonly CritterGrowth _critterGrowth = new(_critterDetail.Species);
    private readonly CritterMortality _critterMortality = new(_critterDetail.Species);
    
    public int Dependants { get; set; }
    public int Workers { get; set; }
    public int Incapacitated { get; set; }
    public int Soldiers { get; set; }
    
    public int Literate { get; set; }
    public int Trained { get; set; }
    
    //public float Workforce => GetWorkforce();

    // public float GetWorkforce()
    // {
    //     float workforceModifier = Class switch
    //     {
    //         CritterClass.Commoners => Species.Workforce,
    //         CritterClass.Bourgeoisie => Species.Workforce,
    //         CritterClass.Elites => Species.Workforce,
    //         _ => throw new ArgumentOutOfRangeException($"Unknown {nameof(Class)}: {Class}")
    //     };
    //     return MathF.Round(Workers * workforceModifier, 2);
    // }

    public void DailyProcessing(ScalarRng sProvinceRng)
    {
        _critterGrowth.DailyProcessing(Details, sProvinceRng);
        SumCritterTotals();
    }
    
    public void WeeklyProcessing(VectorRng vProvinceRng)
    {
        _critterGrowth.WeeklyProcessing(Details);
        _critterMortality.WeeklyProcessing(Details, vProvinceRng);
    }
    
    public void YearlyProcessing()
    {
        Details.AgePopulation();
    }

    private void SumCritterTotals()
    {
        Dependants = Details.Dependants.SumArraySimd();
        Workers = Details.Workers.SumArraySimd();
        Incapacitated = Details.Incapacitated.SumArraySimd();
        Soldiers = Details.Soldiers.SumArraySimd();
    }

    public override string ToString()
    {
        //_critterGrowth.WeeklyProcessing(Details);
        //_critterMortality.WeeklyProcessing(Details, new VectorRng(420));
        //_critterMortality.CalculateWeeklyDeaths(Details);
        //SumCritterTotals();
        return $"Data of {Species.Name}, {Culture}, {Class}: " +
               $"{Dependants}.{Workers}.{Incapacitated}.{Soldiers}|" +
               $"{Literate}.{Trained}|" +
               $"{_critterGrowth}|{_critterMortality}";
    }

    public override int GetHashCode() => HashCode.Combine(_critterDetail);

    public bool Equals(CritterData other) => _critterDetail.Equals(other._critterDetail);
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