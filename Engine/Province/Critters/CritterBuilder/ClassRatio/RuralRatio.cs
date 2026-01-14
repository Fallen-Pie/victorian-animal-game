namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.ClassRatio;

public class RuralRatio : IClassRatio
{
    public (ushort Lower, ushort Middle, ushort Upper) Execute(ushort count)
    {
        ushort lowerCount = count;
        ushort upperCount = (ushort)(lowerCount * 0.03);
        ushort middleCount = (ushort)(lowerCount * 0.12);
        lowerCount = (ushort)(lowerCount - upperCount - middleCount);
        return (lowerCount, middleCount, upperCount);
    }
}