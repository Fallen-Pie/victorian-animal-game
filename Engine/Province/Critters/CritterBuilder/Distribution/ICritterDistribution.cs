using VictorianAnimalGame.Engine.Province.Critters.Species;

namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.Distribution;

public interface ICritterDistribution
{
    public double Execute(short age, SpeciesType speciesDetails);
}