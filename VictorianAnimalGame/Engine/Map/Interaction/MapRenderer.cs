using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Map.Terrain;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map;

public partial class MapRenderer : Sprite2D
{
    private ProvinceId currentHoveredProvinceId;
    private ShaderMaterial mapMaterial;

    public override void _Ready()
    {
        // Grab the material once at startup so we don't have to look it up every frame
        ImageTexture provinceDataTex = RenderMap();
        Texture = provinceDataTex;
        currentHoveredProvinceId = new ProvinceId(0);
        mapMaterial = (ShaderMaterial)Material;
        mapMaterial.SetShaderParameter("border_data_map", provinceDataTex);
        mapMaterial.SetShaderParameter("visual_data_map", provinceDataTex);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // Check if the mouse moved
        if (@event is InputEventMouseMotion)
        {
            MouseMoveEvent();
        }
        
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            MouseLeftClickEvent();
        }

        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.Q)
            {
                Image mapImage = Image.CreateEmpty(MapDefines.MapWidth, MapDefines.MapHeight, false, Image.Format.Rgba8);

                for (int y = 0; y < MapDefines.MapHeight; y++)
                {
                    for (int x = 0; x < MapDefines.MapWidth; x++)
                    {
                        int index = (y * MapDefines.MapWidth) + x;

                        // Get your data
                        ProvinceId provId = MapDefines.ProvinceMapData[index];
                        TerrainType terrain = MapDefines.TerrainMapData[index];

                        // Determine color based on terrain or province owner
                        Color pixelColor = provId.Colour;

                        mapImage.SetPixel(x, y, pixelColor);
                    }
                }
                mapMaterial.SetShaderParameter("visual_data_map", ImageTexture.CreateFromImage(mapImage));
            }
            else if (keyEvent.Keycode == Key.W)
            {
                Image mapImage = Image.CreateEmpty(MapDefines.MapWidth, MapDefines.MapHeight, false, Image.Format.Rgba8);

                for (int y = 0; y < MapDefines.MapHeight; y++)
                {
                    for (int x = 0; x < MapDefines.MapWidth; x++)
                    {
                        int index = (y * MapDefines.MapWidth) + x;

                        // Get your data
                        TerrainType terrain = MapDefines.TerrainMapData[index];

                        // Determine color based on terrain or province owner
                        Color pixelColor = terrain.GetTypography().Colour;

                        mapImage.SetPixel(x, y, pixelColor);
                    }
                }
                mapMaterial.SetShaderParameter("visual_data_map", ImageTexture.CreateFromImage(mapImage));
            }
            else if (keyEvent.Keycode == Key.E)
            {
                Image mapImage = Image.CreateEmpty(MapDefines.MapWidth, MapDefines.MapHeight, false, Image.Format.Rgba8);

                for (int y = 0; y < MapDefines.MapHeight; y++)
                {
                    for (int x = 0; x < MapDefines.MapWidth; x++)
                    {
                        int index = (y * MapDefines.MapWidth) + x;

                        // Get your data
                        TerrainType terrain = MapDefines.TerrainMapData[index];

                        // Determine color based on terrain or province owner
                        Color pixelColor = terrain.GetVegetation().Colour;

                        mapImage.SetPixel(x, y, pixelColor);
                    }
                }
                mapMaterial.SetShaderParameter("visual_data_map", ImageTexture.CreateFromImage(mapImage));
            }
        }
    }

    private void MouseLeftClickEvent()
    {
        Vector2 localClickPos = GetLocalMousePosition();

        if (Offset == Vector2.Zero && Centered)
        {
            localClickPos += Texture.GetSize() / 2;
        }

        int x = Mathf.FloorToInt(localClickPos.X);
        int y = Mathf.FloorToInt(localClickPos.Y);

        if (x >= 0 && x < Texture.GetWidth() && y >= 0 && y < Texture.GetHeight())
        {
            ProvinceId clickedProvinceId = MapDefines.ProvinceMapData[y * Texture.GetWidth() + x];

            if (clickedProvinceId != new ProvinceId(0))
            {
                GD.Print($"User clicked Province ID: {clickedProvinceId}\n" +
                         $"{MapDefines.ProvinceData[clickedProvinceId]}");
            }
        }
    }

    private void MouseMoveEvent()
    {
        Vector2 localMousePos = GetLocalMousePosition();
        
        if (Centered && Texture != null)
        {
            localMousePos += Texture.GetSize() / 2;
        }

        int hoverX = Mathf.Clamp(Mathf.FloorToInt(localMousePos.X), 0, MapDefines.MapWidth - 1);
        int hoverY = Mathf.Clamp(Mathf.FloorToInt(localMousePos.Y), 0, MapDefines.MapHeight - 1);
        int index = (hoverY * MapDefines.MapWidth) + hoverX;
        ProvinceId hoveredProvId = MapDefines.ProvinceMapData[index];

        if (hoveredProvId != currentHoveredProvinceId)
        {
            currentHoveredProvinceId = hoveredProvId;
            mapMaterial.SetShaderParameter("hovered_province_color", hoveredProvId.Colour);
        }
    }

    public ImageTexture RenderMap()
    {
        Image mapImage = Image.CreateEmpty(MapDefines.MapWidth, MapDefines.MapHeight, false, Image.Format.Rgba8);

        for (int y = 0; y < MapDefines.MapHeight; y++)
        {
            for (int x = 0; x < MapDefines.MapWidth; x++)
            {
                int index = (y * MapDefines.MapWidth) + x;

                // Get your data
                ProvinceId provId = MapDefines.ProvinceMapData[index];
                TerrainType terrain = MapDefines.TerrainMapData[index];

                // Determine color based on terrain or province owner
                Color pixelColor = provId.Colour;

                mapImage.SetPixel(x, y, pixelColor);
            }
        }
        return ImageTexture.CreateFromImage(mapImage);
    }
}