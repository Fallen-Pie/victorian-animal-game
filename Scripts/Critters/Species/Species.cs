using System;

namespace VictorianAnimalGame.Scripts.Critters.Species;

public readonly record struct Species
{
    public readonly sbyte AdolescentAge;
    public readonly sbyte AdultAge;
    public readonly sbyte ElderAge;
    private readonly CritterSpecies _speciesType;
    public readonly float FoodConsumption;
    public readonly float WorkforceValue;
    
    //private readonly Consumption _speciesConsumption;
    

    public Species(CritterSpecies species, float foodConsumption, float workforce, 
        sbyte adolescentAge, sbyte adultAge, sbyte elderAge)
    {
        _speciesType = species;
        FoodConsumption = foodConsumption;
        WorkforceValue = workforce;
        AdolescentAge = adolescentAge;
        AdultAge = adultAge;
        ElderAge = elderAge;
    }

    public float GetWorkforce()
    {
        return WorkforceValue;
    }

    public override string ToString()
    {
        return $"Name:{_speciesType}|Food:{FoodConsumption}|Work:{WorkforceValue}|" +
               $"Age:{AdolescentAge}/{AdultAge}/{ElderAge}";
    }
}

public class Consumption
{
    
}