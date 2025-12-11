using System;
using System.Runtime.InteropServices;

namespace VictorianAnimalGame.Scripts.Critters
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly record struct CritterEntry
    {
        private readonly CritterCulture _culture;
        private readonly CritterSpecies _species;
        private readonly CritterOccupation _occupation;
        private readonly CritterDetails _critterDetails;

        public CritterEntry(CritterSpecies newSpecies, CritterOccupation newOccupation, 
            CritterCulture newCulture, CritterDetails newDetails)
        {
            _species = newSpecies;
            _occupation = newOccupation;
            _culture = newCulture;
            _critterDetails = newDetails;
        }

        public CritterDetails GetCritterDetails()
        {
            return _critterDetails;
        }
        
        public CritterOccupation GetCritterOccupation()
        {
            return _occupation;
        }
        
        public CritterSpecies GetCritterSpecies()
        {
            return _species;
        }
        
        public uint GetCritterCount()
        {
            return _critterDetails.GetCritterCount();
        }
        
        public bool Equals(CritterEntry newCritter) =>
            (_culture, _species, _occupation).Equals(
                (newCritter._culture, newCritter._species, newCritter._occupation));

        public override int GetHashCode()
        {
            return HashCode.Combine(_culture, _species, _occupation);
        }

        public override string ToString()
        {
            return $"Info on this {_species}: " +
                   $"{_culture}" +
                   $"/{_occupation}" +
                   $"/{GetHashCode()}/{_critterDetails}";
        }
    }
}
