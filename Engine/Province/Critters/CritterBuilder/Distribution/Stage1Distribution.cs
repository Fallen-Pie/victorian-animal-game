namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.Distribution;

public class Stage1Distribution : ICritterDistribution
{
    public double Execute(short age, Species.Species species)
    {
        if (age < species.AdultAge)
        {
            return 1.0 - (0.4 * ((double)age / species.AdultAge)); 
        }
        return 0.8 * (1.0 - (double)(age - species.AdultAge) / (species.ElderAge * 1.2 - species.FertileAge));
    }
}