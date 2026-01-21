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
        private CritterDetails _critterDetails;
        public SpeciesType Species { get; }
        public CultureType Culture { get; }
        
        public CritterLifeStage LifeStage => GetCritterAge();
        
        public CritterEntry(short newYear, SpeciesType newSpecies, 
            CultureType newCulture, CritterDetails newDetails)
        {
            _year = newYear;
            Species = newSpecies;
            Culture = newCulture;
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
            return Species;
        }
        
        public CultureType GetCritterCulture()
        {
            return Culture;
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
            CritterDefines.Species.TryGetValue(Species, out var species);
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
            (_culture: Culture, _species: Species, _year).Equals(
                (newCritter.Culture, newCritter.Species, newCritter._year));

        public override int GetHashCode()
        {
            return HashCode.Combine(Culture, Species, _year);
        }

        public override string ToString()
        {
            return $"Current {CritterDefines.Species[Species].SpeciesName}: " +
                   $"{Culture}" +
                   $"/{_year}" +
                   $"/{GetHashCode()}/{LifeStage}/{_critterDetails}";
        }
    }
}
