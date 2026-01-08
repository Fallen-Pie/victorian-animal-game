using System;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Critters
{
    [StructLayout(LayoutKind.Sequential)]
    public record struct CritterEntry
    {
        private readonly short _year;
        private readonly SpeciesType _species;
        private readonly CritterCulture _culture;
        private CritterDetails _critterDetails;
        
        public CritterEntry(short newYear, SpeciesType newSpecies, 
            CritterCulture newCulture, CritterDetails newDetails)
        {
            _year = newYear;
            _species = newSpecies;
            _culture = newCulture;
            _critterDetails = newDetails;
        }
        
        public short GetCritterYear()
        {
            return _year;
        }

        public CritterDetails GetCritterDetails()
        {
            return _critterDetails;
        }
        
        public SpeciesType GetCritterSpecies()
        {
            return _species;
        }
        
        public uint GetCritterCount()
        {
            return _critterDetails.GetCritterCount();
        }

        private CritterLifeStage GetCritterAge()
        {
            CritterDefines.Species.TryGetValue(_species, out var species);
            int age = DateDefines.Year - _year;
            if (age < species.AdolescentAge)
            {
                return CritterLifeStage.Young;
            }
            if (age < species.AdultAge)
            {
                return CritterLifeStage.Adolescent;
            }
            if (age < species.ElderAge)
            {
                return CritterLifeStage.Adult;
            }

            return CritterLifeStage.Elder;
        }
        
        public bool Equals(CritterEntry newCritter) =>
            (_culture, _species, _year).Equals(
                (newCritter._culture, newCritter._species, newCritter._year));

        public override int GetHashCode()
        {
            return HashCode.Combine(_culture, _species, _year);
        }

        public override string ToString()
        {
            return $"Current {CritterDefines.Species[_species].SpeciesName}: " +
                   $"{_culture}" +
                   $"/{_year}" +
                   $"/{GetHashCode()}/{GetCritterAge()}/{_critterDetails}";
        }
    }
}
