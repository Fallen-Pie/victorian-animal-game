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
    public static readonly FrozenDictionary<uint, ProvinceId> ProvinceMapping = DefineProvinces();
    public static readonly ProvinceId[] ProvinceMapData = DefineMapData();

    private static FrozenDictionary<uint, ProvinceId> DefineProvinces()
    {
        List<ProvinceConfig> data = YamlReader.ReadFiles<ProvinceConfig>("Data/Map/Provinces/");
        Dictionary<uint, ProvinceId> h = [];
        
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
            
            h.Add(new Color(provinceData.Colour).ToUint(), new ProvinceId(provinceData.Id));
            
            // SpeciesType newSpeciesType = new SpeciesType(i);
            // speciesTypes.Add(speciesData.Name, newSpeciesType);
            //
            // SpeciesDetails newSpeciesDetails = new SpeciesBuilder()
            //     .SetSpeciesName(speciesData.Name)
            //     .SetSpeciesType(newSpeciesType)
            //     .SetAges(speciesData.Ages)
            //     .SetPeakFertility(speciesData.PeakFertility)
            //     .SetFoodConsumption(speciesData.FoodConsumption)
            //     .SetWorkforceValue(speciesData.WorkforceValue)
            //     .Build();
            // species.Add(newSpeciesType, newSpeciesDetails);
            //
            // Console.WriteLine(newSpeciesDetails);
        }
        
        //TODO Read these from files
        // return new Dictionary<uint, ProvinceId>
        // {
        //     { new Color("FF7F27").ToUint(), new ProvinceId(1) }, // Orange
        //     { new Color("ED1C24").ToUint(), new ProvinceId(2) }, // Red
        //     { new Color("880015").ToUint(), new ProvinceId(3) }, // Dark Red
        //     
        //     { new Color("22B14C").ToUint(), new ProvinceId(4) }, // Light Green
        //     { new Color("1FA046").ToUint(), new ProvinceId(5) }, // Green
        //     { new Color("B5E61D").ToUint(), new ProvinceId(6) }, // Yellow Green
        //     { new Color("008000").ToUint(), new ProvinceId(7) }, // Forest Green
        //     { new Color("006400").ToUint(), new ProvinceId(8) }, // Dark Green
        // }.ToFrozenDictionary();

        return h.ToFrozenDictionary();
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