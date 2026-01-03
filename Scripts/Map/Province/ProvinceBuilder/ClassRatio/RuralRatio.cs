namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.ClassRatio;

public class RuralRatio : IClassRatio
{
    public (uint Lower, uint Middle, uint Upper) Execute(uint count)
    {
        uint lowerCount = count;
        uint upperCount = (uint)(lowerCount * 0.03);
        uint middleCount = (uint)(lowerCount * 0.12);
        lowerCount = lowerCount - upperCount - middleCount;
        return (lowerCount, middleCount, upperCount);
    }
}