using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Defines;

public static class CritterDefines
{
    public static readonly FrozenDictionary<CritterSpecies, Species> Species = CreateFrozenSpecies();
    // public static readonly FrozenDictionary<CritterSpecies, Species> Goods = CreateFrozenGoods();
    //
    // private static FrozenDictionary<CritterSpecies, Species> CreateFrozenGoods()
    // {
    //     throw new System.NotImplementedException();
    // }

    private static FrozenDictionary<CritterSpecies, Species> CreateFrozenSpecies()
    {
        Dictionary<CritterSpecies, Species> species = [];
        species.Add(CritterSpecies.Otter, new Species(CritterSpecies.Otter, 1, 1.1f,
            6, 10, 13, 22));
        species.Add(CritterSpecies.Beaver, new Species(CritterSpecies.Beaver, 1, 0.9f,
            5, 10, 12, 15));
        species.Add(CritterSpecies.Raccoon, new Species(CritterSpecies.Raccoon, 1, 1, 
            5, 10, 12, 15));
        return species.ToFrozenDictionary();
    }
}

public static class MapDefines
{
    private static short _currentYear = 1819;
        
    public static short GetCurrentYear()
    {
        return _currentYear;
    }

    private static void IncreaseCurrentYear()
    {
        _currentYear += 1;
    }
}