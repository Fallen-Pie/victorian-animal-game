namespace VictorianAnimalGame.Scripts.Critters.Species;

public readonly record struct Species
{
    private readonly CritterSpecies _speciesType;
    private readonly float _foodConsumption;
    private readonly float _workforceValue;
    //private readonly Consumption _speciesConsumption;

    public Species(CritterSpecies species, float foodConsumption, float workforce)
    {
        _speciesType = species;
        _foodConsumption = foodConsumption;
        _workforceValue = workforce;
    }

    public float GetWorkforce()
    {
        return _workforceValue;
    }

    public override string ToString()
    {
        return $"Name:{_speciesType}|Food:{_foodConsumption}|Work:{_workforceValue}";
    }
}

public class Consumption
{
    
}