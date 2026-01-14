namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.Distribution;

public interface ICritterDistribution
{
    public double Execute(short age, Species.Species species);
}