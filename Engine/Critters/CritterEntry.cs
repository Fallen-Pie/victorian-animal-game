using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Critters.Cultures;
using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Critters
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly record struct CritterEntry(SpeciesType Species, CultureType Culture, CritterClass Class)
    {
        public SpeciesType Species { get; } = Species;
        public CritterClass Class { get; } = Class;
        public CultureType Culture { get; } = Culture;
        public CritterDetails Details { get; } = new(Species);
        
        public bool Equals(CritterEntry newCritter) =>
            (Culture, Species, Class).Equals(
                (newCritter.Culture, newCritter.Species, newCritter.Class));

        public override int GetHashCode()
        {
            return HashCode.Combine(Culture, Species, Class);
        }

        public override string ToString()
        {
            string critterDetails = $"Current {Class} {Species.Name} {Culture}/{GetHashCode()}";
            for (int i = 0; i < Details.Length; i++)
            {
                critterDetails += new CritterView(Details, i).ToString();
            }
            return critterDetails;
        }
    }
}
