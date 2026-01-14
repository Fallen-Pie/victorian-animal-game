using System;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters.Cultures;
using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.Critters
{
    [StructLayout(LayoutKind.Sequential)]
    public record struct CritterEntry
    {
        private readonly short _year;
        private readonly SpeciesType _species;
        private readonly CultureType _culture;
        private CritterDetails _critterDetails;
        
        public CritterEntry(short newYear, SpeciesType newSpecies, 
            CultureType newCulture, CritterDetails newDetails)
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
        
        public CultureType GetCritterCulture()
        {
            return _culture;
        }
        
        public uint GetCritterClassCount(CritterClass critterClass)
        {
            return _critterDetails.GetCritterCount(critterClass);
        }
        
        public uint GetCritterTotalCount()
        {
            return _critterDetails.GetCritterTotalCount();
        }

        public CritterLifeStage GetCritterAge()
        {
            CritterDefines.Species.TryGetValue(_species, out var species);
            int age = DateDefines.Year - _year;

            return age switch
            {
                _ when age < species.AdolescentAge => CritterLifeStage.Young,
                _ when age < species.AdultAge => CritterLifeStage.Adolescent,
                _ when age < species.ElderAge => CritterLifeStage.Adult,
                _ => CritterLifeStage.Elder
            };
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
