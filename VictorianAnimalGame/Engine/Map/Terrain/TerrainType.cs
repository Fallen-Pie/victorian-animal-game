using System;
using VictorianAnimalGame.Engine.Map.Terrain.Climate;
using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Map.Terrain.Vegetation;

namespace VictorianAnimalGame.Engine.Map.Terrain;

public readonly record struct TerrainType()
{
    private readonly TypographyType _typographyData;
    private readonly VegetationType _vegetationData;
    private readonly ClimateType _climateType;

    public TerrainType(TypographyType newTypography, VegetationType newVegetation) : this()
    {
        _typographyData = newTypography;
        _vegetationData = newVegetation;
        //_climateType = newClimate;
    }

    public VegetationType GetVegetation()
    {
        return _vegetationData;
    }
    
    public TypographyType GetTypography()
    {
        return _typographyData;
    }
    
    // public ClimateType GetClimate()
    // {
    //     return _climateType;
    // }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(_typographyData, _vegetationData);
    }

    public override string ToString()
    {
        return $"{_typographyData.Name}-{_vegetationData.Name}";
    }
}