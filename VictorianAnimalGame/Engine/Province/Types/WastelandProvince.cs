using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public class WastelandProvince(
    ProvinceId newId,
    uint newMapColour,
    string newName,
    uint pixelSize,
    Dictionary<ProvinceId, uint> provinceNeighbours,
    Vector2I provinceCentre)
    : IProvince(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceCentre)
{
    protected override bool Traversable => false;
    protected override bool Communications => false;
}