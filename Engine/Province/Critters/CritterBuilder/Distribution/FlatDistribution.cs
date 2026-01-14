namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.Distribution;

public class FlatDistribution : ICritterDistribution
{
    public double Execute(short age, Species.Species species)
    {
        if (age <= species.FertileAge)
        {
            return 1.0 - (0.2 * ((double)age / species.FertileAge)); 
        }
        return 0.8 * (1.0 - (double)(age - species.FertileAge) / (species.ElderAge * 1.2 - species.FertileAge));
    }
}