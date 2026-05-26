using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using VictorianAnimalGame.Engine.Algorithms;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Types;

namespace VictorianAnimalGame.View.Nodes.Map;

public partial class DebugProvincePath : Node2D
{
    private ProvinceId? _startProvince;
    private List<Vector2> _pathPoints = [];
    //private Line2D pathLine;

    // public override void _Ready()
    // {
    //     pathLine = GetNode<Line2D>("../Line2D");
    // }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Right)
        {
            Vector2 localMousePos = GetLocalMousePosition();
            
            int clickX = Mathf.Clamp(Mathf.FloorToInt(localMousePos.X), 0, MapDefines.MapWidth - 1);
            int clickY = Mathf.Clamp(Mathf.FloorToInt(localMousePos.Y), 0, MapDefines.MapHeight - 1);
            int index = (clickY * MapDefines.MapWidth) + clickX;

            ProvinceId clickedId = MapDefines.ProvinceMapData[index];
            
            if (_startProvince == null)
            {
                _startProvince = clickedId;
                _pathPoints.Clear();
                QueueRedraw();
                GD.Print($"Start point set: {clickedId}");
            }
            else
            {
                ProvinceId endProvince = clickedId;
                GD.Print($"End point set: {endProvince}. Calculating path...");
                
                CalculateAndDrawPath(_startProvince.Value, endProvince);
                _startProvince = null; 
            }
        }
    }

    private void CalculateAndDrawPath(ProvinceId start, ProvinceId end)
    {
        List<(ProvinceId, uint)> pathData = Pathfinding.GetPathLand(start, end);
        _pathPoints.Clear();
        
        foreach ((ProvinceId provId, _) in pathData)
        {
            IProvince provinceData = MapDefines.ProvinceData[provId];
            _pathPoints.Add(provinceData.Centre);
        }
        
        QueueRedraw();
        //pathLine.Points = _pathPoints.ToArray();
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
        DrawPolyline(_pathPoints.ToArray(), lineColor, lineWidth, true);

        // Draw the dots at the Centre of each province
        foreach (Vector2 point in _pathPoints)
        {
            DrawCircle(point, dotRadius, dotColor, true);
        }
    }
}