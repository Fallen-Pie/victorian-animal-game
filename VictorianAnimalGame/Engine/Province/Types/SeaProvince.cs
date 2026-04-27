using System;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public class SeaProvince(
    ProvinceId newId,
    uint newMapColour,
    string newName,
    uint pixelSize,
    Dictionary<ProvinceId, uint> provinceNeighbours,
    Vector2I provinceCentre)
    : IProvince(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceCentre)
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