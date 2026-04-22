using System;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public class SeaProvince : IProvince
{
    public SeaProvince(ProvinceId newId, uint newMapColour, string newName, uint pixelSize, 
        Dictionary<ProvinceId, uint> provinceNeighbours, Vector2I provinceCentre) : 
        base(newId, newMapColour, newName, pixelSize, provinceNeighbours, provinceCentre)
    {
        
    }

    public string GetDetails()
    {
        throw new NotImplementedException();
    }

    public void SetName()
    {
        throw new NotImplementedException();
    }
}