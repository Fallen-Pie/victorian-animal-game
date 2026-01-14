namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.ClassRatio;

public interface IClassRatio
{
    public (ushort Lower, ushort Middle, ushort Upper) Execute(ushort count);
}