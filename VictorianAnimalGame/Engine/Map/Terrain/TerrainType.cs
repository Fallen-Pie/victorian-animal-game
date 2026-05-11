using System;

namespace VictorianAnimalGame.Engine.Map.Terrain;

public readonly record struct TerrainType(TypographyType newTypography, VegetationType newVegetation)
{
    private readonly TypographyType _typographyData = newTypography;
    private readonly VegetationType _vegetationData = newVegetation;

    public override int GetHashCode()
    {
        return HashCode.Combine(_typographyData, _vegetationData);
    }

    public override string ToString()
    {
        return $"{_typographyData.Name}-{_vegetationData.Name}";
    }
}