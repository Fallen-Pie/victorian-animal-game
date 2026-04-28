using System;
using System.Collections.Generic;
using Godot;

namespace VictorianAnimalGame.Engine.Province.Types;

public class DeepOceanProvince(
    ProvinceId newId,
    uint newMapColour,
    string newName,
    uint pixelSize,
    Dictionary<ProvinceId, uint> provinceNeighbours,
    Vector2I provinceCentre)
    : Sea(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceCentre)
{
    protected override bool Traversable => false;
    protected override bool Communications => false;
}