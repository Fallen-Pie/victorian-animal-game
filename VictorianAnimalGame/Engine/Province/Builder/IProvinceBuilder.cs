using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Province.Types;

namespace VictorianAnimalGame.Engine.Province.Builder;

public abstract class IProvinceBuilder
{
    protected ProvinceId ProvinceId;
    protected uint MapColour;
    protected string Name;
    
    protected uint Size;
    protected Dictionary<ProvinceId, uint> Neighbours;
    protected Vector2I Centre;

    public IProvinceBuilder SetProvinceId(ProvinceId newProvinceId)
    {
        ProvinceId = newProvinceId;
        return this;
    }
    
    public IProvinceBuilder SetColour(uint newMapColour)
    {
        MapColour = newMapColour;
        return this;
    }
    
    public IProvinceBuilder SetName(string newMame)
    {
        Name = newMame;
        return this;
    }

    public IProvinceBuilder SetSize(uint newSize)
    {
        Size = newSize;
        return this;
    }
    
    public IProvinceBuilder SetNeighbours(Dictionary<ProvinceId, uint> newNeighbours)
    {
        Neighbours = newNeighbours;
        return this;
    }
    
    public IProvinceBuilder SetCentre(Vector2I newCentre)
    {
        Centre = newCentre;
        return this;
    }
    
    public abstract IProvince Build();
}

public class LandProvinceBuilder : IProvinceBuilder
{
    public override LandProvince Build()
    {
        return new LandProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}

public class SeaProvinceBuilder : IProvinceBuilder
{
    public override SeaProvince Build()
    {
        return new SeaProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}

public class WastelandProvinceBuilder : IProvinceBuilder
{
    public override WastelandProvince Build()
    {
        return new WastelandProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}
