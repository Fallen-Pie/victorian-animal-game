using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Map.Terrain;

namespace VictorianAnimalGame.Engine.Province.Types;

public abstract class Land(
    ProvinceId newId, 
    uint newMapColour, 
    string newName, 
    uint pixelSize, 
    Dictionary<ProvinceId, uint> provinceNeighbours, 
    Dictionary<TerrainType, uint> provinceTerrain,
    Vector2I provinceCentre)
    : IProvince(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceTerrain, provinceCentre)
{
    
}