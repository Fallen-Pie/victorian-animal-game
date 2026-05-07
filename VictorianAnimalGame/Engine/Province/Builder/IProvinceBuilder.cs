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
    public override HabitableProvince Build()
    {
        return new HabitableProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}

public class SeaProvinceBuilder : IProvinceBuilder
{
    public override OceanProvince Build()
    {
        return new OceanProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}

public class WastelandProvinceBuilder : IProvinceBuilder
{
    public override InhospitableProvince Build()
    {
        return new InhospitableProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}

public class LakeProvinceBuilder : IProvinceBuilder
{
    public override LakeProvince Build()
    {
        return new LakeProvince(ProvinceId, MapColour, Name, Size, Neighbours, Centre);
    }
}
