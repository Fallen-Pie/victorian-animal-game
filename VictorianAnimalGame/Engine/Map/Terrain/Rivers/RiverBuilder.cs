using Godot;
using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Terrain.Rivers;

public class RiverBuilder(string name, Vector2I startPoint)
{
    public string name = name;
    public Vector2I startPoint = startPoint;
    
    public Vector2I endPoint;
    public uint length = 1;

    public RiverDirection CurrentBehind = RiverDirection.None;
    public uint CurrentPositionData;

    public TypographyType startRiver;
    public TypographyType endRiver;
    public ProvinceId endProvince;
    
    public void Step(RiverDirection newDirection)
    {
        CurrentBehind = (RiverDirection)(((int)newDirection + 2) % 4);
        length++;
    }

    public RiverDetails Build()
    {
        return new RiverDetails(name, startPoint, endPoint, length);
    }
}