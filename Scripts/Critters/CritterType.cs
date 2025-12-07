using System;
using System.Text.RegularExpressions;

namespace VictorianAnimalGame.Scripts.Critters
{
    public class CritterType(CritterSpecies newSpecies, CritterOccupation newOccupation, CritterCulture newCulture) : IEquatable<CritterType>
    {
        public CritterSpecies Species = newSpecies;
        public CritterOccupation Occupation = newOccupation;
        public CritterCulture Culture = newCulture;

        public override string ToString()
        {
            return $"Info on this Critter:\nHashCode: {GetHashCode()}\nDetails: {Species}/{Culture}/{Occupation}";
        }

        public override int GetHashCode()        
        {
            return HashCode.Combine(Species, Culture, Occupation);
        }

        public override bool Equals(object obj)
        {
            return obj is CritterType other && Equals(other);
        }

        public bool Equals(CritterType other)
        {
            return Species == other.Species && Culture == other.Culture && Occupation == other.Occupation;
        }

        public static bool operator ==(CritterType left, CritterType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CritterType left, CritterType right)
        {
            return !(left == right);
        }
    }
}
