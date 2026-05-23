using Godot;

namespace VictorianAnimalGame.Engine.Map.Terrain.Typography;

public record TypographyDetails() : ITerrain
{
    public string Name { get; }
    public Color Colour { get; }
    
    private TypographyType Type { get; }

    public TypographyDetails(string newName, string newMapColour, TypographyType newType) : this()
    {
        Name = newName;
        Colour = Color.FromHtml(newMapColour);
        Type = newType;
    }

    public override string ToString() => $"Typography(Name:{Name}|Colour:#{Colour.ToHtml(false)}|Type:{Type})";
}