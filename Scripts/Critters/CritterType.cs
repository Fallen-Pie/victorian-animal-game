namespace VictorianAnimalGame.Scripts.Critters
{
    public readonly record struct CritterType
    {
        private readonly CritterCulture _culture;
        private readonly CritterSpecies _species;
        private readonly CritterOccupation _occupation;

        public CritterType(CritterSpecies newSpecies, CritterOccupation newOccupation, CritterCulture newCulture)
        {
            _species = newSpecies;
            _occupation = newOccupation;
            _culture = newCulture;
        }
        
        public override string ToString()
        {
            return $"Info on this Critter:\nHashCode: {GetHashCode()}\nDetails: " +
                   $"{_species}/" +
                   $"{_culture}" +
                   $"/{_occupation}";
        }
    }
}
