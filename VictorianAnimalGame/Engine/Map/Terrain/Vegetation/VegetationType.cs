using System;
using Godot;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Map.Terrain.Vegetation;

public readonly record struct VegetationType(byte VegetationId)
{
    private VegetationDetails Vegetation => MapDefines.TerrainVegetation[this];
    
    public string Name => Vegetation.Name;
    public Color Colour => Vegetation.Colour;
    
    public override string ToString() => VegetationId.ToString();
}