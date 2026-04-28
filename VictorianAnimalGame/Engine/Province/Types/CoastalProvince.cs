using System;
using System.Collections.Generic;
using Godot;

namespace VictorianAnimalGame.Engine.Province.Types;

public class CoastalProvince(
    ProvinceId newId,
    uint newMapColour,
    string newName,
    uint pixelSize,
    Dictionary<ProvinceId, uint> provinceNeighbours,
    Vector2I provinceCentre)
    : Sea(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceCentre)
{
    protected override bool Traversable => true;
    protected override bool Communications => true;
}