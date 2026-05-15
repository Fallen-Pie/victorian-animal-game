using System;
using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Map.Terrain.Vegetation;

namespace VictorianAnimalGame.Engine.Map.Terrain;

public readonly record struct TerrainType()
{
    private readonly TypographyType _typographyData;
    private readonly VegetationType _vegetationData;

    public TerrainType(TypographyType newTypography, VegetationType newVegetation) : this()
    {
        _typographyData = newTypography;
        _vegetationData = newVegetation;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(_typographyData, _vegetationData);
    }

    public override string ToString()
    {
        return $"{_typographyData.Name}-{_vegetationData.Name}";
    }
}