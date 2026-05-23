using Godot;

namespace VictorianAnimalGame.Engine.Map.Terrain;

public interface ITerrain
{ 
    string Name { get; }
    Color Colour { get; }
}