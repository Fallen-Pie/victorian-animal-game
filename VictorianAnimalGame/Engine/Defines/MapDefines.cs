using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.FileReader.DataReader;

namespace VictorianAnimalGame.Engine.Defines;

public static class MapDefines
{
    public static readonly FrozenDictionary<uint, ProvinceType> ProvinceMapping = DefineProvinces();
    public static readonly ProvinceType[] ProvinceMapData = DefineMapData();

    private static FrozenDictionary<uint, ProvinceType> DefineProvinces()
    {
        //TODO Read these from files
        return new Dictionary<uint, ProvinceType>
        {
            { new Color("FF7F27").ToUint(), new ProvinceType(1) }, // Orange
            { new Color("ED1C24").ToUint(), new ProvinceType(2) }, // Red
            { new Color("880015").ToUint(), new ProvinceType(3) }, // Dark Red
            
            { new Color("22B14C").ToUint(), new ProvinceType(4) }, // Light Green
            { new Color("1FA046").ToUint(), new ProvinceType(5) }, // Green
            { new Color("B5E61D").ToUint(), new ProvinceType(6) }, // Yellow Green
            { new Color("008000").ToUint(), new ProvinceType(7) }, // Forest Green
            { new Color("006400").ToUint(), new ProvinceType(8) }, // Dark Green
        }.ToFrozenDictionary();
    }
    
    private static ProvinceType[] DefineMapData()
    {
        Image provincesImage = Image.LoadFromFile("Data/Map/provinces.bmp");
        
        //TODO Make this Reusable
        return MapReader.Scan(provincesImage);

        //Image riverImage = Image.LoadFromFile("res://maps/rivers.png");
        // 4. Run the River Strategy, passing in the province data
        //var riverStrategy = new RiverDetectionStrategy(new Color("0000FF"), provinceMapData); // Blue rivers
        //HashSet<int> riverProvinces = MapScanner.Scan(riverImage, riverStrategy);
    
        //GD.Print($"Province 1 has river: {riverProvinces.Contains(1)}");
    }
}