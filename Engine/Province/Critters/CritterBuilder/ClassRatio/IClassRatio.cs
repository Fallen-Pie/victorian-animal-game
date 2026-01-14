namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.ClassRatio;

public interface IClassRatio
{
    public (uint Lower, uint Middle, uint Upper) Execute(uint count);
}