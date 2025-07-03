using System;
using System.Text.RegularExpressions;

namespace VictorianAnimalGame.Scripts.Critters
{
    public struct CritterKey
    {
        public byte year;
        public CritterSpecies species;
        public CritterCulture culture;
        public CritterOccupation occupation;

        public readonly string GetDetails() {
            return String.Format("Info on this Critter:\nHashCode: {0}\nYear: {1}\nDetails: {2}/{3}/{4}", GetHashCode(), year, species, culture, occupation);
        }
    }

    public class CritterKeyBuilder 
    {
        private CritterKey _critterKey = new();

        public CritterKeyBuilder SetYear(byte newYear)
        {
            _critterKey.year = newYear;
            return this;
        }
        public CritterKeyBuilder SetSpecies(CritterSpecies newSpecies)
        {
            _critterKey.species = newSpecies;
            return this;
        }
        public CritterKeyBuilder SetCulture(CritterCulture newCulture)
        {
            _critterKey.culture = newCulture;
            return this;
        }
        public CritterKeyBuilder SetOccupation(CritterOccupation newOccupation)
        {
            _critterKey.occupation = newOccupation;
            return this;
        }
        public CritterKey Build() 
        {
            return _critterKey;
        }
    }
}
