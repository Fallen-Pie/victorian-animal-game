namespace VictorianAnimalGame.Scripts.Critters
{
    public enum CritterSpecies : ushort
    {
        None = 0,
        Otter,
        Beaver,
        Raccoon
    }

    public enum CritterClass : byte
    {
        UPPER,
        MIDDLE,
        LOWER,
        NONE
    }

    public enum CritterOccupation : ushort
    {
        None = 0,
        Dependent,
        Labourer,
        Worker,
        Aristocrats,
        Bureaucrats
    }
    
    public enum CritterCulture : ushort
    {
        None = 0,
        OtterAmericans,
        BeaverAmericans,
        RaccoonAmericans
    }
}
