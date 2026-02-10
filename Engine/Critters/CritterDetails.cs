using System;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Critters
{
    public record CritterDetails
    {
        public readonly ushort[] Dependants;
        public readonly ushort[] Workers;
        public readonly ushort[] Soldiers;
        public readonly ushort[] Incapacitated;
        
        public readonly ushort[] Literate;
        public readonly ushort[] Trained;
        
        public int Length => Dependants.Length;

        public CritterDetails(SpeciesType newSpecies)
        {
            int maxSize = newSpecies.MaxAge + 1;
            Dependants = new ushort[maxSize];
            Workers = new ushort[maxSize];
            Incapacitated = new ushort[maxSize];
            Soldiers = new ushort[maxSize];
            Literate = new ushort[maxSize];
            Trained = new ushort[maxSize];
        }
    }

    
}
