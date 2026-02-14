using VictorianAnimalGame.Engine.Critters.Species;

namespace VictorianAnimalGame.Engine.Critters.CritterBuilder.Distribution;

public class Stage1Distribution : ICritterDistribution
{
    public double Execute(short age, SpeciesType speciesDetails)
    {
        if (age < speciesDetails.AdultAge)
        {
            return 1.0 - (0.4 * ((double)age / speciesDetails.AdultAge)); 
        }
        return 0.8 * (1.0 - (double)(age - speciesDetails.AdultAge) / (speciesDetails.ElderAge * 1.2 - speciesDetails.FertileAge));
    }
}