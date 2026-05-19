using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Terrain.Rivers;

public class RiverBuilder(string name, Vector2I startPoint, ProvinceId startProvince)
{
    public string name = name;
    public TypographyType type;
    public Vector2I startPoint = startPoint;
    public ProvinceId startProvince = startProvince;
    
    public ProvinceId endProvince;
    public Vector2I endPoint;
    public uint length = 1;

    public RiverDirection CurrentBehind = RiverDirection.None;

    public TypographyType? startRiver;
    public TypographyType? endRiver;

    public List<Vector2I> deltaEndpoints = [];
    public bool riverEndpoint = false;
    
    public void Step(RiverDirection newDirection)
    {
        CurrentBehind = (RiverDirection)(((int)newDirection + 2) % 4);
        length++;
    }

    public RiverBuilder SetEndPoint(Vector2I newEndPoint)
    {
        endPoint = newEndPoint;
        return this;
    }
    
    public RiverBuilder SetEndProvince(ProvinceId newEndProvince)
    {
        endProvince = newEndProvince;
        return this;
    }
    
    public void AddDeltaEndpoint(Vector2I newEndpoint)
    {
        deltaEndpoints.Add(newEndpoint);
    }
    
    public void SetEndRiver(TypographyType newEndRiver)
    {
        endRiver = newEndRiver;
    }

    public RiverDetails Build()
    {
        return new RiverDetails(name, type, startPoint, endPoint, length, 
            startProvince, endProvince, deltaEndpoints, startRiver, endRiver);
    }

    public RiverBuilder AddType(TypographyType newType)
    {
        type = newType;
        return this;
    }
}