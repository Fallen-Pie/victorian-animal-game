using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.FileReader.MapReader;
using VictorianAnimalGame.FileReader.MapReader.Strategies;

namespace VictorianAnimalGame.Engine.Defines;

public static class MapDefines
{
    public static readonly int[] provinceMapData = DefineProvinces();
    
    private static int[] DefineProvinces()
    {
        // 1. Load your maps
        Image provincesImage = Image.LoadFromFile("Data/Map/provinces.bmp");
        //Image riverImage = Image.LoadFromFile("res://maps/rivers.png");

        // 2. Define your predetermined provinces (usually loaded from a JSON or CSV)
        var provinceDefinitions = new Dictionary<uint, int>
        {
            // Strip alpha from your definition colors so they match the scanner logic
            { MapScanner.ColorToMapUint(new Color("FF7F27")) & 0x00FFFFFF, 1 }, // Orange
            { MapScanner.ColorToMapUint(new Color("ED1C24")) & 0x00FFFFFF, 2 }, // Red
            { MapScanner.ColorToMapUint(new Color("880015")) & 0x00FFFFFF, 3 }, // Dark Red
            
            { MapScanner.ColorToMapUint(new Color("22B14C")) & 0x00FFFFFF, 4 }, // Light Green
            { MapScanner.ColorToMapUint(new Color("1FA046")) & 0x00FFFFFF, 5 }, // Green
            { MapScanner.ColorToMapUint(new Color("B5E61D")) & 0x00FFFFFF, 6 }, // Yellow Green
            { MapScanner.ColorToMapUint(new Color("008000")) & 0x00FFFFFF, 7 }, // Forest Green
            { MapScanner.ColorToMapUint(new Color("006400")) & 0x00FFFFFF, 8 }, // Dark Green
        }.ToFrozenDictionary();

        // 3. Run the Province Strategy
        var provinceStrategy = new ProvinceAssignmentStrategy(provinceDefinitions);
        return MapScanner.Scan(provincesImage, provinceStrategy);

        // 4. Run the River Strategy, passing in the province data
        //var riverStrategy = new RiverDetectionStrategy(new Color("0000FF"), provinceMapData); // Blue rivers
        //HashSet<int> riverProvinces = MapScanner.Scan(riverImage, riverStrategy);
    
        //GD.Print($"Province 1 has river: {riverProvinces.Contains(1)}");
    }
}