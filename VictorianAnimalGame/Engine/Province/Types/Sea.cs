using System.Collections.Generic;
using Godot;

namespace VictorianAnimalGame.Engine.Province.Types;

public abstract class Sea(
    ProvinceId newId, 
    uint newMapColour, 
    string newName, 
    uint pixelSize, 
    Dictionary<ProvinceId, uint> provinceNeighbours, 
    Vector2I provinceCentre)
    : IProvince(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceCentre)
{
    
}