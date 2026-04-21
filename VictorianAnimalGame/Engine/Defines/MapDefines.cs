using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Builder;
using VictorianAnimalGame.Engine.Province.Types;
using VictorianAnimalGame.FileReader.DataReader;
using VictorianAnimalGame.FileReader.DataReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class MapDefines
{
    public static readonly FrozenDictionary<uint, ProvinceId> ColourMapping;
    public static readonly FrozenDictionary<ProvinceId, IProvince> ProvinceMapping;
    public static readonly ProvinceId[] ProvinceMapData = DefineMapData();

    static MapDefines()
    {
        var (colourMapping, idMapping) = DefineProvinces();
        ColourMapping = colourMapping;
        ProvinceMapping = idMapping;
    }

    private static (FrozenDictionary<uint, ProvinceId>, FrozenDictionary<ProvinceId, IProvince>) DefineProvinces()
    {
        List<ProvinceConfig> data = YamlReader.ReadFiles<ProvinceConfig>("Data/Map/Provinces/");
        Dictionary<uint, ProvinceId> colourMapping = [];
        Dictionary<ProvinceId, IProvince> idMapping = [];
        
        foreach (var province in data)
        {
            var provinceData = province.Province;
            IProvinceBuilder builder;
            
            switch (provinceData.Type)
            {
                case "Land":
                    builder = new LandProvinceBuilder();
                    break;
                case "Sea":
                    builder = new SeaProvinceBuilder();
                    break;
                case "Wasteland":
                    builder = new WastelandProvinceBuilder();
                    break;
                default:
                    throw new ArgumentException("Unknown province type: " + provinceData.Type);
            }
            
            IProvince newProvince = builder.SetColour(new Color(provinceData.Colour).ToUint())
                .SetName(provinceData.Name)
                .SetProvinceId(new ProvinceId(provinceData.Id))
                .Build();
            
            colourMapping.Add(new Color(provinceData.Colour).ToUint(), new ProvinceId(provinceData.Id));
            idMapping.Add(new ProvinceId(provinceData.Id), newProvince);
        }

        return (colourMapping.ToFrozenDictionary(), idMapping.ToFrozenDictionary());
    }
    
    private static ProvinceId[] DefineMapData()
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