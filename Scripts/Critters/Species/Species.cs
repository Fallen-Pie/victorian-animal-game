using System;

namespace VictorianAnimalGame.Scripts.Critters.Species;

public readonly record struct Species
{
    public readonly sbyte AdolescentAge;
    public readonly sbyte AdultAge;
    public readonly sbyte FertileAge;
    public readonly sbyte ElderAge;
    
    public readonly CritterSpecies SpeciesType;
    public readonly float FoodConsumption;
    public readonly float WorkforceValue;
    
    //private readonly Consumption _speciesConsumption;
    

    public Species(CritterSpecies species, float foodConsumption, float workforce, 
        sbyte adolescentAge, sbyte adultAge, sbyte fertileAge, sbyte elderAge)
    {
        SpeciesType = species;
        FoodConsumption = foodConsumption;
        WorkforceValue = workforce;
        AdolescentAge = adolescentAge;
        AdultAge = adultAge;
        FertileAge = fertileAge;
        ElderAge = elderAge;
    }

    public float GetWorkforce()
    {
        return WorkforceValue;
    }

    public override string ToString()
    {
        return $"Name:{SpeciesType}|Food:{FoodConsumption}|Work:{WorkforceValue}|" +
               $"Age:{AdolescentAge}/{AdultAge}/{ElderAge}";
    }
}

public class Consumption
{
    
}