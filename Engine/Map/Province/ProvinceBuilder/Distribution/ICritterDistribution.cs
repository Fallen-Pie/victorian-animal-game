using VictorianAnimalGame.Scripts.Critters.Species;

namespace VictorianAnimalGame.Engine.Map.Province.ProvinceBuilder.Distribution;

public interface ICritterDistribution
{
    public double Execute(short age, Species species);
}