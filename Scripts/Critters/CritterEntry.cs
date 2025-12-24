using System;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Scripts.Defines;

namespace VictorianAnimalGame.Scripts.Critters
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly record struct CritterEntry
    {
        private readonly short _year;
        private readonly CritterSpecies _species;
        private readonly CritterCulture _culture;
        private readonly CritterDetails _critterDetails;
        
        public CritterEntry(short newYear, CritterSpecies newSpecies, 
            CritterCulture newCulture, CritterDetails newDetails)
        {
            _year = newYear;
            _species = newSpecies;
            _culture = newCulture;
            _critterDetails = newDetails;
            UpdateCritterYear();
        }
        
        public short GetCritterYear()
        {
            return _year;
        }

        public CritterDetails GetCritterDetails()
        {
            return _critterDetails;
        }
        
        public CritterSpecies GetCritterSpecies()
        {
            return _species;
        }
        
        public uint GetCritterCount()
        {
            return _critterDetails.GetCritterCount();
        }

        public void UpdateCritterYear()
        {
            CritterDefines.Species.TryGetValue(_species, out var species);
            int age = MapDefines.GetCurrentYear() - _year;
            if (age < species.AdolescentAge)
            {
                _critterDetails.SetCritterAge(CritterLifeStage.Young);
            }
            else if (age < species.AdultAge)
            {
                _critterDetails.SetCritterAge(CritterLifeStage.Adolescent);
            } 
            else if (age < species.ElderAge)
            {
                _critterDetails.SetCritterAge(CritterLifeStage.Adult);
            }
            else
            {
                _critterDetails.SetCritterAge(CritterLifeStage.Elder);
            }
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
            return $"Current {_species}: " +
                   $"{_culture}" +
                   $"/{_year}" +
                   $"/{GetHashCode()}/{_critterDetails}";
        }
    }
}
