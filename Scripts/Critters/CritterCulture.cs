namespace VictorianAnimalGame.Scripts.Critters;

public record struct CritterCultureOld
{
    private readonly CritterCulture _name;

    public CritterCultureOld(CritterCulture newName)
    {
        _name = newName;
    }
}