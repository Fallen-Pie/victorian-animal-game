using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Map.Terrain;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public class InhospitableProvince(
    ProvinceId newId, 
    uint newMapColour, 
    string newName, 
    uint pixelSize, 
    Dictionary<ProvinceId, uint> provinceNeighbours, 
    Dictionary<TerrainType, uint> provinceTerrain,
    Vector2I provinceCentre)
    : Land(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceTerrain, provinceCentre)
{
    protected override bool Traversable => false;
    protected override bool Communications => false;
}