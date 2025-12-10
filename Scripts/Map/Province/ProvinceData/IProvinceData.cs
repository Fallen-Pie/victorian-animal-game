using System;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

public interface IProvinceData
{
    public void AddAmount(uint newAmount);
}

public record WorkforceData : IProvinceData
{
    public WorkforceData(CritterOccupation newOccupations, uint newCount)
    {
        Occupations = newOccupations;
        Amount = newCount;
    }

    private CritterOccupation Occupations { get; }
    private uint Amount { get; set; }
    
    public virtual bool Equals(WorkforceData newCritter) =>
        Occupations == newCritter.Occupations;


    public override int GetHashCode()
    {
        return HashCode.Combine(Occupations);
    }

    public override string ToString()
    {
        return $"{Occupations} workforce: {Amount}";
    }

    public void AddAmount(uint newAmount)
    {
        Amount += newAmount;
    }
}