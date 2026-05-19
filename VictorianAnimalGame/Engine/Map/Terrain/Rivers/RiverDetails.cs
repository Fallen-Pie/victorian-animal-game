using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Terrain.Rivers;

public record RiverDetails() : ITerrain
{
    public string Name { get; }
    public string Colour { get; }
    public uint Length { get; }
    public TypographyType Type { get; }
    
    public Vector2I StartPoint { get; }
    public TypographyType? StartRiver { get; }
    public ProvinceId StartProvince { get; }
    
    public Vector2I EndPoint { get; }
    public TypographyType? EndRiver { get; }
    public ProvinceId EndProvince { get; }
    
    public List<Vector2I> DeltaEndpoints { get; }

    public RiverDetails(string newName,
        TypographyType newType,
        Vector2I newStartPoint, 
        Vector2I newEndPoint, 
        uint newLength,
        ProvinceId startProvince,
        ProvinceId endProvince,
        List<Vector2I> deltaEndpoints,
        TypographyType? startRiver, 
        TypographyType? endRiver) : this()
    {
        Name = newName;
        Colour = "FFFFFF";
        Length = newLength;
        Type = newType;
        
        StartPoint = newStartPoint;
        StartRiver = startRiver;
        StartProvince = startProvince;
        
        EndPoint = newEndPoint;
        EndRiver = endRiver;
        EndProvince = endProvince;
        
        DeltaEndpoints = deltaEndpoints;
    }

    public override string ToString()
    {
        string riverString = $"River(Name:{Name}|Colour:{Colour}|Type:{Type}|Length:{Length}|" +
                             $"StartPoint:{StartPoint}|StartProvince:{StartProvince.Name}|";
        if (StartRiver != null)
        {
            riverString += $"StartRiver:{StartRiver}|";
        }
        riverString += $"EndPoint:{EndPoint}|EndProvince:{EndProvince.Name}";
        if (EndRiver != null)
        {
            riverString += $"|EndRiver:{EndRiver}";
        }
        for (int i = 1; i <= DeltaEndpoints.Count; i++)
        {
            riverString += $"|DeltaEndpoints:{DeltaEndpoints[i - 1]}";
        }
        riverString += $")";
        return riverString;
    }
}