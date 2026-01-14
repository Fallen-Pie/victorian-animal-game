using System;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.Cultures;
using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public record struct CritterData
{
    public CritterData(CultureType newCulture, CritterLifeStage newLifeStage, 
        SpeciesType newSpecies, CritterClass newClass, uint newCount)
    {
        Culture = newCulture;
        LifeStage = newLifeStage;
        Species = newSpecies;
        Class = newClass;
        Count = newCount;
    }
    
    public CultureType Culture { get; }
    public CritterLifeStage LifeStage { get; }
    public SpeciesType Species { get; }
    public CritterClass Class { get; }
    public uint Count { get; set; }
    
    public bool Equals(CritterData newData) =>
        (_culture: Culture, _species: Species, _lifeStage: LifeStage, _class: Class).Equals(
            (newData.Culture, newData.Species, newData.LifeStage, newData.Class));
    
    public override int GetHashCode() => HashCode.Combine(Culture, Species, LifeStage, Class);

    public override string ToString()
    {
        return $"Critter Details of {Culture} {CritterDefines.Species[Species].SpeciesName}: {LifeStage}/{Class}/{Count}";
    }
}