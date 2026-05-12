using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Map.Terrain;

namespace VictorianAnimalGame.Engine.Province.Types;

public class LakeProvince(
    ProvinceId newId, 
    uint newMapColour, 
    string newName, 
    uint pixelSize, 
    Dictionary<ProvinceId, uint> provinceNeighbours, 
    Dictionary<TerrainType, uint> provinceTerrain,
    Vector2I provinceCentre)
    : Sea(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceTerrain, provinceCentre)
{
    protected override bool Traversable => false;
    protected override bool Communications => true;
}