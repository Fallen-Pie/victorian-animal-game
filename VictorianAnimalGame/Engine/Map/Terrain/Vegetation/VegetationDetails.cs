namespace VictorianAnimalGame.Engine.Map.Terrain.Vegetation;

public readonly record struct VegetationDetails() : ITerrain
{
    public string Name { get; }
    public string Colour { get; }
    
    public VegetationType Type { get; }

    public VegetationDetails(string newName, string newMapColour, VegetationType newType) : this()
    {
        Name = newName;
        Colour = newMapColour;
        Type = newType;
    }

    public override string ToString() => $"Vegetation(Name:{Name}|Colour:#{Colour}|Type:{Type})";
}