using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.Distribution;

public interface ICritterDistribution
{
    public double Execute(short age, Species species);
}