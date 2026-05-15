using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using Microsoft.VisualBasic.FileIO;

namespace VictorianAnimalGame.Engine.Defines.MapCreation;

public class RiverCreation
{
    public RiverCreation()
    {
        GetRiverData();
    }

    private void GetRiverData()
    {
        ReadOnlySpan<uint> RiverData = MapDefines.GetImageData(Image.LoadFromFile("Data/Map/Views/rivers.bmp"));
        
    }

    private HashSet<RiverConfig> GetRiverConfig()
    {
        HashSet<RiverConfig> riverData = [];
        using TextFieldParser csvParser = new TextFieldParser("Data/Map/Rivers/Rivers.csv");
        csvParser.SetDelimiters(",");
        csvParser.ReadLine();

        while (!csvParser.EndOfData)
        {
            // Read current line fields, pointer moves to the next line.
            string[] fields = csvParser.ReadFields();
            if (fields != null)
            {
                RiverConfig newRiver = new RiverConfig(Convert.ToInt32(fields[1]), Convert.ToInt32(fields[2]), fields[0]);
                riverData.Add(newRiver);
            }
            else
            {
                throw new NullReferenceException("Data/Map/Rivers/Rivers.csv is empty");
            }
        }

        return riverData;
    }
}

internal record struct RiverConfig
{
    Vector2I StartPosition { get; }
    private string Name { get; set; }

    public RiverConfig(int x, int y, string name)
    {
        StartPosition = new Vector2I(x, y);
        Name = name;
    }

    public override int GetHashCode()
    {
        return StartPosition.GetHashCode();
    }
}