using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.View.Nodes.Map;

public partial class MapInteraction : Sprite2D
{
    public override void _UnhandledInput(InputEvent @event)
    {
        // Listen for left mouse clicks
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            // 1. Get the click position relative to the Sprite's top-left corner
            Vector2 localClickPos = GetLocalMousePosition();

            // Adjust for offset if your sprite is centered
            if (Offset == Vector2.Zero && Centered)
            {
                localClickPos += Texture.GetSize() / 2;
            }

            // 2. Convert to integer pixel coordinates
            int x = Mathf.FloorToInt(localClickPos.X);
            int y = Mathf.FloorToInt(localClickPos.Y);

            // 3. Ensure the click is within the map bounds
            if (x >= 0 && x < Texture.GetWidth() && y >= 0 && y < Texture.GetHeight())
            {
                // 4. Look up the province ID instantly
                ProvinceId clickedProvinceId = MapDefines.ProvinceMapData[y * Texture.GetWidth() + x];

                if (clickedProvinceId != new ProvinceId(0))
                {
                    GD.Print($"User clicked Province ID: {clickedProvinceId}\n" +
                             $"{MapDefines.ProvinceData[clickedProvinceId]}");
                    // Fire an event, open a UI menu, select an army, etc.
                }
            }
        }
    }
}