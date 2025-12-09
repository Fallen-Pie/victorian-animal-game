namespace VictorianAnimalGame.Scripts.Critters.Species;

public class BaseSpecies : ISpecies
{
    private readonly float _foodConsumption = 1;
    private readonly float _workforceValue = 1;
    
    private string _speciesName;
    private CritterSpecies _critterSpecies;
    //private list<Goods>  _goods;
}