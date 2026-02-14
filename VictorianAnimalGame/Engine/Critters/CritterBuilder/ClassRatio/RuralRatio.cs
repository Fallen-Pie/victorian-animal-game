using System.Collections.Generic;

namespace VictorianAnimalGame.Engine.Critters.CritterBuilder.ClassRatio;

public class RuralRatio : IClassRatio
{
    public List<int> Execute(uint count)
    {
        ushort lowerCount = (ushort)count;
        ushort upperCount = (ushort)(lowerCount * 0.03);
        ushort middleCount = (ushort)(lowerCount * 0.12);
        lowerCount = (ushort)(lowerCount - upperCount - middleCount);
        return [lowerCount, middleCount, upperCount];
    }
}