namespace VictorianAnimalGame.Scripts.Critters.Species;

public readonly record struct Species
{
    public readonly byte AdolescentAge;
    public readonly byte AdultAge;
    public readonly byte FertileAge;
    public readonly byte ElderAge;
    
    public readonly SpeciesType SpeciesType;
    public readonly float FoodConsumption;
    public readonly float WorkforceValue;
    
    public readonly string SpeciesName;
    
    //private readonly Consumption _speciesConsumption;
    

    public Species(SpeciesType species, float foodConsumption, float workforce, 
        byte adolescentAge, byte adultAge, byte fertileAge, byte elderAge, string speciesName)
    {
        SpeciesType = species;
        FoodConsumption = foodConsumption;
        WorkforceValue = workforce;
        AdolescentAge = adolescentAge;
        AdultAge = adultAge;
        FertileAge = fertileAge;
        ElderAge = elderAge;
        SpeciesName = speciesName;
    }

    public float GetWorkforce()
    {
        return WorkforceValue;
    }

    public override string ToString()
    {
        return $"Name:{SpeciesName}|Food:{FoodConsumption}|Work:{WorkforceValue}|" +
               $"Age:{AdolescentAge}/{AdultAge}/{ElderAge}";
    }
}