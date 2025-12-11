using System;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public record struct WorkforceSpeciesData : IProvinceData
{
    public WorkforceSpeciesData(CritterOccupation newOccupations, CritterSpecies newSpecies, uint newCount)
    {
        Occupations = newOccupations;
        Species = newSpecies;
        Amount = newCount;
    }

    private CritterOccupation Occupations { get; }
    private CritterSpecies Species { get; }
    private uint Amount { get; set; }
    
    public bool Equals(WorkforceSpeciesData newCritter) =>
        (Species, Occupations).Equals(
            (newCritter.Species, newCritter.Occupations));

    public override int GetHashCode()
    {
        return HashCode.Combine(Occupations, Species);
    }

    public override string ToString()
    {
        return $"{Species} {Occupations} workforce: {Amount}";
    }

    public void AddAmount(uint newAmount)
    {
        Amount += newAmount;
    }
}