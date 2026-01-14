using System;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public record struct WorkforceData
{
    public WorkforceData(SpeciesType newSpecies, CritterClass newClass, uint newCount)
    {
        Species = newSpecies;
        Class = newClass;
        CreateWorkforce(newCount);
    }
    
    private SpeciesType Species { get; }
    private CritterClass Class { get; }
    public float WorkforceAmount { get; set; }

    private void CreateWorkforce(uint critterCount)
    {
        WorkforceAmount = MathF.Round(critterCount * CritterDefines.Species[Species].WorkforceValue, 2);
    }
    
    public void AddWorkforce(float workforceAmount)
    {
        WorkforceAmount += workforceAmount;
    }
    
    public bool Equals(WorkforceData newData) =>
        (Species, Class).Equals((newData.Species, newData.Class));
    
    public override int GetHashCode() => HashCode.Combine(Species, Class);
    
    public override string ToString()
    {
        return $"Workforce of {Class} {CritterDefines.Species[Species].SpeciesName}: {WorkforceAmount}";
    }
}