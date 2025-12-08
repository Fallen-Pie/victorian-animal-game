namespace VictorianAnimalGame.Scripts.Critters;

public record struct CritterCulture
{
    private readonly Culture _name;

    public CritterCulture(Culture newName)
    {
        _name = newName;
    }
}