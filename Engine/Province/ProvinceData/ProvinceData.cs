using System;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.Cultures;
using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public record struct ProvinceData
{
    private CultureType Culture { get; }
    private CritterLifeStage LifeStage { get; }
    private SpeciesType Species { get; }
    private CritterClass Class { get; }
    public uint Count { get; set; }

    public ProvinceData(CultureType newCulture, CritterLifeStage newLifeStage, 
        SpeciesType newSpecies, CritterClass newClass, uint newCount)
    {
        Culture = newCulture;
        LifeStage = newLifeStage;
        Species = newSpecies;
        Class = newClass;
        Count = newCount;
    }

    public void AddCount(uint newCount)
    {
        Count += newCount;
    }
    
    public bool Equals(ProvinceData newData) =>
        (_culture: Culture, _species: Species, _lifeStage: LifeStage, _class: Class).Equals(
            (newData.Culture, newData.Species, newData.LifeStage, newData.Class));
    
    public override int GetHashCode() => HashCode.Combine(Culture, Species, LifeStage, Class);

    public override string ToString()
    {
        return $"Current {Species}: {LifeStage}/{Class}/{Culture}/{Count}";
    }
}