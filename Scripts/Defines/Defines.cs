using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Defines;

public static class CritterDefines
{
    public static readonly FrozenDictionary<CritterSpecies, Species> Species = CreateFrozenDictionary();
    
    private static FrozenDictionary<CritterSpecies, Species> CreateFrozenDictionary()
    {
        Dictionary<CritterSpecies, Species> species = [];
        species.Add(CritterSpecies.Otter, new Species(CritterSpecies.Otter, 1, 1.1f));
        species.Add(CritterSpecies.Beaver, new Species(CritterSpecies.Beaver, 1, 0.9f));
        species.Add(CritterSpecies.Raccoon, new Species(CritterSpecies.Raccoon, 1, 1));
        return species.ToFrozenDictionary();
    }
}