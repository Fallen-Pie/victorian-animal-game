namespace VictorianAnimalGame.Engine.Critters.Species;

public readonly record struct SpeciesDetails(
    SpeciesType SpeciesType,
    float FoodConsumption,
    float WorkforceValue,
    byte AdolescentAge,
    byte AdultAge,
    byte FertileAge,
    byte ElderAge,
    string SpeciesName)
{
    public readonly byte AdolescentAge = AdolescentAge;
    public readonly byte AdultAge = AdultAge;
    public readonly byte FertileAge = FertileAge;
    public readonly byte ElderAge = ElderAge;
    
    public readonly SpeciesType SpeciesType = SpeciesType;
    public readonly float FoodConsumption = FoodConsumption;
    public readonly float WorkforceValue = WorkforceValue;
    
    public readonly string SpeciesName = SpeciesName;
    
    //private readonly Consumption _speciesConsumption;


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