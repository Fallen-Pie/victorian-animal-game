using Godot;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Map.Terrain.Typography;

public readonly record struct TypographyType(ushort TypographyId)
{
    private ITerrain Typography => MapDefines.TerrainTypography[this];
    
    public string Name => Typography.Name;
    public Color Colour => Typography.Colour;
    
    public override string ToString() => TypographyId.ToString();
}