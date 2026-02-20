using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Critters.CritterBuilder.Distribution;

public interface ICritterDistribution
{
    public double Execute(short age, SpeciesType speciesDetails);
}