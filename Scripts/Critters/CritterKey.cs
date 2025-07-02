using System;

namespace VictorianAnimalGame.Scripts.Critters
{
    internal struct CritterKey
    {
        private byte year;
        private CritterSpecies species;
        private CritterCulture culture;
        private CritterOccupation occupation;

        public CritterKey SetYear(byte newYear)
        {
            year = newYear;
            return this;
        }
        public CritterKey SetSpecies(CritterSpecies newSpecies)
        {
            species = newSpecies;
            return this;
        }
        public CritterKey SetCulture(CritterCulture newCulture)
        {
            culture = newCulture;
            return this;
        }
        public CritterKey SetOccupation(CritterOccupation newOccupation)
        {
            occupation = newOccupation;
            return this;
        }
        public readonly string GetDetails() {
            return String.Format(@"Info on this Critter:
                HashCode: {0}
                Year: {1}
                Details: {2}/{3}/{4}", GetHashCode(), year, species, culture, occupation);
        }
    }
}
