using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public static readonly FrozenDictionary<ProvinceId, IProvince> ProvinceData;
    public static readonly ProvinceId[] ProvinceMapData;

    private static int MapWidth;
    private static int MapHeight;

    static MapDefines()
    {
        List<ProvinceConfig> data = YamlReader.ReadFiles<ProvinceConfig>("Data/Map/Provinces/");
        
        ColourMapping = MapColours(data);
        ProvinceMapData = DefinePixelData();
        ProvinceData = DefineProvinces(data);
        foreach (IProvince newProvince in ProvinceData.Values)
        {
            GD.Print(newProvince);
        }
    }

    private static FrozenDictionary<uint, ProvinceId> MapColours(List<ProvinceConfig> data)
    {
        
        Dictionary<uint, ProvinceId> colourMapping = [];

        foreach (var province in data)
        {
            var provinceData = province.Province;
            colourMapping.Add(new Color(provinceData.Colour).ToUint(), new ProvinceId(provinceData.Id));
        }

        return colourMapping.ToFrozenDictionary();
    }

    private static FrozenDictionary<ProvinceId, IProvince> DefineProvinces(List<ProvinceConfig> data)
    {
        Dictionary<ProvinceId, IProvince> idMapping = [];
        Dictionary<ProvinceId, ProvinceBuilderDetails> provinceDetails = [];

        foreach (ProvinceId provinceId in ColourMapping.Values)
        {
            provinceDetails.Add(provinceId, new ProvinceBuilderDetails());
        }
        
        int index = 0;
        foreach (ProvinceId pixelId in ProvinceMapData)
        {
            ProvinceBuilderDetails details = provinceDetails[pixelId];
            
            int x = index % MapWidth;
            int y = index / MapHeight;
            details.AddPixel(x, y);
            
            if (x < MapWidth - 1) CheckNeighbor(ProvinceMapData[index + 1]);
            if (y < MapHeight - 1) CheckNeighbor(ProvinceMapData[index + MapWidth]);
            
            void CheckNeighbor(ProvinceId neighborProvId)
            {
                if (neighborProvId != pixelId)
                {
                    details.AddNeighbours(neighborProvId);
                    provinceDetails[neighborProvId].AddNeighbours(pixelId);
                }
            }

            index++;
        }

        Dictionary<ProvinceId, Vector2I> provinceCentres = [];
        foreach (var (provinceId, details) in provinceDetails) 
        {
            details.GetCentre();
            provinceCentres.Add(provinceId, details.Centre);
        }

        foreach (var details in provinceDetails.Values)
        {
            details.GetNeighboursDistance(ref provinceCentres);
        }
        
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
                case "Lake":
                    builder = new LakeProvinceBuilder();
                    break;
                default:
                    throw new ArgumentException("Unknown province type: " + provinceData.Type);
            }
            
            ProvinceId newId = new ProvinceId(provinceData.Id);
            ProvinceBuilderDetails newDetails = provinceDetails[newId];
            var (pixelSize, provinceNeighbours, provinceCentre) = newDetails;
            
            IProvince newProvince = builder.SetColour(new Color(provinceData.Colour).ToUint())
                .SetName(provinceData.Name)
                .SetProvinceId(newId)
                .SetSize(pixelSize)
                .SetNeighbours(provinceNeighbours)
                .SetCentre(provinceCentre)
                .Build();
            
            idMapping.Add(new ProvinceId(provinceData.Id), newProvince);
        }

        return idMapping.ToFrozenDictionary();
    }
    
    private static ProvinceId[] DefinePixelData()
    {
        Image provincesImage = Image.LoadFromFile("Data/Map/Views/provinces.bmp");
        MapWidth = provincesImage.GetWidth();
        MapHeight = provincesImage.GetHeight();
        
        //TODO Make this Reusable
        return MapReader.Scan(provincesImage);

        //Image riverImage = Image.LoadFromFile("res://maps/rivers.png");
        // 4. Run the River Strategy, passing in the province data
        //var riverStrategy = new RiverDetectionStrategy(new Color("0000FF"), provinceMapData); // Blue rivers
        //HashSet<int> riverProvinces = MapScanner.Scan(riverImage, riverStrategy);
    
        //GD.Print($"Province 1 has river: {riverProvinces.Contains(1)}");
        
    }
}