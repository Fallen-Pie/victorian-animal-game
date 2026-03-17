using VictorianAnimalGame.Engine.Components.Critters.Species;

namespace VictorianAnimalGame.Engine.Components.Critters.CritterBuilder.Distribution;

public interface ICritterDistribution
{
    public double Execute(short age, SpeciesType speciesDetails);
}