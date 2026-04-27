using System;
using System.Collections.Generic;
using Godot;

namespace VictorianAnimalGame.Engine.Province.Types;

public class OceanProvince(
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
    
    public string GetDetails()
    {
        throw new NotImplementedException();
    }

    public void SetName()
    {
        throw new NotImplementedException();
    }


}