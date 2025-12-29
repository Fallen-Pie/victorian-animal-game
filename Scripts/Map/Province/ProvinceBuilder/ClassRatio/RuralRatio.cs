namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.ClassRatio;

public class RuralRatio : IClassRatio
{
    public (uint Lower, uint Middle, uint Upper) Execute(uint count)
    {
        uint upperCount = (uint)(count * 0.03);
        uint middleCount = (uint)(count * 0.12);
        count = count - upperCount - middleCount;
        return (count, middleCount, upperCount);
    }
}