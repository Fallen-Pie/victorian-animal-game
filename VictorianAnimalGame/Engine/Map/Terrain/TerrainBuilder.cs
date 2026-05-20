using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Map.Terrain.Vegetation;

namespace VictorianAnimalGame.Engine.Map.Terrain;

public class TerrainBuilder
{
    
}

public class TypographyBuilder
{
    private string _name;
    private string _colour;
    private TypographyType _type;
    
    public TypographyBuilder SetTypographyName(string name)
    {
        _name = name;
        return this;
    }
    
    public TypographyBuilder SetTypographyColour(string colour)
    {
        _colour = colour;
        return this;
    }
    
    public TypographyBuilder SetTypographyType(TypographyType type)
    {
        _type = type;
        return this;
    }
    
    public TypographyDetails Build()
    {
        return new TypographyDetails(_name, _colour, _type);
    }
}

public class VegetationBuilder
{
    private string _name;
    private string _colour;
    private VegetationType _type;
    
    public VegetationBuilder SetVegetationName(string name)
    {
        _name = name;
        return this;
    }
    
    public VegetationBuilder SetVegetationColour(string colour)
    {
        _colour = colour;
        return this;
    }
    
    public VegetationBuilder SetVegetationType(VegetationType type)
    {
        _type = type;
        return this;
    }
    
    public VegetationDetails Build()
    {
        return new VegetationDetails(_name, _colour, _type);
    }
}