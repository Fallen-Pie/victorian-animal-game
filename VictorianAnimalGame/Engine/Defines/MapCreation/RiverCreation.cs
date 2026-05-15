using System;
using System.Collections.Generic;
using Godot;
using Microsoft.VisualBasic.FileIO;
using VictorianAnimalGame.Engine.Extensions;

namespace VictorianAnimalGame.Engine.Defines.MapCreation;

public class RiverCreation
{
    private uint baseRiver = new Color("09477f").ToUint();
    
    private uint startRiver = new Color("f52738").ToUint();
    private uint mergeRiver = new Color("54d43b").ToUint();
    private uint splitRiver = new Color("f59e27").ToUint();
    private uint deltaRiver = new Color("10be9d").ToUint();
    private uint endRiver = new Color("9f27ce").ToUint();
    
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