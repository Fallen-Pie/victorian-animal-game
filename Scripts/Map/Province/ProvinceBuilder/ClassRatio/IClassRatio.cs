namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.ClassRatio;

public interface IClassRatio
{
    public (uint Lower, uint Middle, uint Upper) Execute(uint count);
}