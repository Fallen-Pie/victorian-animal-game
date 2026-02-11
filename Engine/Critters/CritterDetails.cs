using System;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Critters;

public record CritterDetails
{
    private readonly SpeciesType _species;
    
    public readonly ushort[] Dependants;
    public readonly ushort[] Workers;
    public readonly ushort[] Soldiers;
    public readonly ushort[] Incapacitated;
        
    public readonly ushort[] Literate;
    public readonly ushort[] Trained;
        
    public int Length => _species.MaxAge + 1;

    public CritterDetails(SpeciesType newSpecies)
    {
        _species = newSpecies;
        int maxSize = (Length + 15) & ~15;
        Dependants = new ushort[maxSize];
        Workers = new ushort[maxSize];
        Incapacitated = new ushort[maxSize];
        Soldiers = new ushort[maxSize];
        Literate = new ushort[maxSize];
        Trained = new ushort[maxSize];
    }
    
    public void AgePopulation(SpeciesType newSpecies)
    {
        for (int i = _species.MaxAge; i > 0; i--)
        {
            Dependants[i] = Dependants[i - 1];
            Workers[i] = Workers[i - 1];
            Incapacitated[i] = Incapacitated[i - 1];
            Soldiers[i] = Soldiers[i - 1];
            Literate[i] = Literate[i - 1];
            Trained[i] = Trained[i - 1];
        }

        Dependants[0] = 0;
        Workers[0] = 0;
        Incapacitated[0] = 0;
        Soldiers[0] = 0;
        Literate[0] = 0;
        Trained[0] = 0;
        
        Dependants[Length] = 0;
        Workers[Length] = 0;
        Incapacitated[Length] = 0;
        Soldiers[Length] = 0;
        Literate[Length] = 0;
        Trained[Length] = 0;
    }
}

