namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public record struct CritterBirthRate
{
    public float BirthRate;

    public override string ToString()
    {
        return $"Current birth rate: {BirthRate}";
    }
}