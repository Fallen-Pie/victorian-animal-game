using System;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Algorithms;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Types;

namespace VictorianAnimalGame.Engine.Map.Interaction;

public partial class DebugProvincePath : Node2D
{
    // Adjust these references to point to wherever your actual data lives
    private ProvinceId? _startProvince;
    private List<Vector2> _pathPoints = [];

    public override void _UnhandledInput(InputEvent @event)
    {
        // Listen for Right-Click
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Right)
        {
            // 1. Get the mouse position and convert to your 1D array index
            Vector2 localMousePos = GetLocalMousePosition();
            
            // Assuming the parent is your centered map sprite
            //Sprite2D mapSprite = GetParent<Sprite2D>();
            // if (mapSprite.Centered && mapSprite.Texture != null)
            // {
            //     localMousePos += mapSprite.Texture.GetSize() / 2;
            // }

            int clickX = Mathf.Clamp(Mathf.FloorToInt(localMousePos.X), 0, MapDefines.MapWidth - 1);
            int clickY = Mathf.Clamp(Mathf.FloorToInt(localMousePos.Y), 0, MapDefines.MapHeight - 1);
            
            // Map dimensions - replace with your actual variables
            int index = (clickY * MapDefines.MapWidth) + clickX;

            // Get the clicked province ID
            ProvinceId clickedId = MapDefines.ProvinceMapData[index];

            // 2. Handle the Start -> End logic
            if (_startProvince == null)
            {
                // First click: Set start point and clear old lines
                _startProvince = clickedId;
                _pathPoints.Clear();
                QueueRedraw(); // Clear the screen
                GD.Print($"Start point set: {clickedId}");
            }
            else
            {
                // Second click: Set end point, calculate, and draw
                ProvinceId endProvince = clickedId;
                GD.Print($"End point set: {endProvince}. Calculating path...");
                
                CalculateAndDrawPath(_startProvince.Value, endProvince);
                
                // Reset start province so the next right-click starts a new path
                _startProvince = null; 
            }
        }
    }

    private void CalculateAndDrawPath(ProvinceId start, ProvinceId end)
    {
        // Replace this with your actual pathfinding call
        List<(ProvinceId, uint)> pathData = Pathfinding.GetPathLand(start, end);

        _pathPoints.Clear();

        // CAUTION: Standard C# Dictionaries do not guarantee strict ordering. 
        // If your pathData was inserted in Start -> End order, we extract them here.
        // If your dictionary keys are unordered, you will need to sort them before this step 
        // so the line doesn't zig-zag wildly!
        foreach ((ProvinceId provId, _) in pathData)
        {
            // Lookup your full Province object to get the Centre
            IProvince provinceData = MapDefines.ProvinceData[provId];
            
            // Convert Godot's Vector2I (int) to Vector2 (float) for drawing
            _pathPoints.Add(provinceData.Centre);
        }

        // Tell Godot to trigger the _Draw() function this frame
        QueueRedraw();
    }

    public override void _Draw()
    {
        // Don't draw if we don't have enough points for a line
        if (_pathPoints.Count < 2) return;

        Color lineColor = new Color(1, 0, 0); // Solid Red
        float lineWidth = 2.0f;
        Color dotColor = new Color(1, 1, 0);  // Solid Yellow
        float dotRadius = 3.0f;

        // Draw the connecting lines
        foreach (Vector2 point in _pathPoints)
            GD.Print(point);
        DrawPolyline(_pathPoints.ToArray(), lineColor, lineWidth, true);

        // Draw the dots at the Centre of each province
        foreach (Vector2 point in _pathPoints)
        {
            DrawCircle(point, dotRadius, dotColor, true);
        }
    }
}