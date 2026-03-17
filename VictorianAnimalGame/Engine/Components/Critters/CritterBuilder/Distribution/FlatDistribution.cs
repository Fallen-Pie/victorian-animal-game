using VictorianAnimalGame.Engine.Components.Critters.Species;

namespace VictorianAnimalGame.Engine.Components.Critters.CritterBuilder.Distribution;

public class FlatDistribution : ICritterDistribution
{
    public double Execute(short age, SpeciesType speciesDetails)
    {
        if (age <= speciesDetails.FertileAge)
        {
            return 1.0 - (0.2 * ((double)age / speciesDetails.FertileAge)); 
        }
        return 0.8 * (1.0 - (double)(age - speciesDetails.FertileAge) / (speciesDetails.ElderAge * 1.2 - speciesDetails.FertileAge));
    }
}