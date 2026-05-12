using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Godot;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Map.Terrain;
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
    
    public static readonly FrozenDictionary<TypographyType, TypographyDetails> TerrainTypography;
    public static readonly FrozenDictionary<VegetationType, VegetationDetails> TerrainVegetation;
    public static readonly TerrainType[] TerrainMapData;

    private static int MapWidth;
    private static int MapHeight;

    static MapDefines()
    {
        List<ProvinceConfig> data = YamlReader.ReadFiles<ProvinceConfig>("Data/Map/Provinces/");
        ColourMapping = MapColours(data);
        ProvinceMapData = DefinePixelData();
        
        TerrainTypography = DefineTypography();
        TerrainVegetation = DefineVegetation();
        TerrainMapData = DefineTerrain();
        
        ProvinceData = DefineProvinces(data);
        // foreach (IProvince newProvince in ProvinceData.Values)
        // {
        //     GD.Print(newProvince);
        // }
    }

    private static FrozenDictionary<TypographyType, TypographyDetails> DefineTypography()
    {
        List<TypographyConfig> data = YamlReader.ReadFiles<TypographyConfig>("Data/Map/Typography/");
        Dictionary<TypographyType, TypographyDetails> terrainTypography = [];

        TypographyType emptyType = new TypographyType(0);
        terrainTypography.Add(emptyType, 
            new TypographyBuilder()
                .SetTypographyName("Empty")
                .SetTypographyColour("000000")
                .SetTypographyType(emptyType)
                .Build()
            );
        
        for (byte i = 1; i <= data.Count; i++)
        {
            var typographyData = data[i - 1].Typography;
            
            TypographyType newType = new TypographyType(i);
            TypographyDetails newDetails = new TypographyBuilder()
                .SetTypographyName(typographyData.Name)
                .SetTypographyColour(typographyData.Colour)
                .SetTypographyType(newType)
                .Build();

            Console.WriteLine(newDetails.ToString());
            terrainTypography.Add(newType, newDetails);
        }
        
        return terrainTypography.ToFrozenDictionary();
    }
    
    private static FrozenDictionary<VegetationType, VegetationDetails> DefineVegetation()
    {
        List<VegetationConfig> data = YamlReader.ReadFiles<VegetationConfig>("Data/Map/Vegetation/");
        Dictionary<VegetationType, VegetationDetails> terrainVegetation = [];

        VegetationType emptyType = new VegetationType(0);
        terrainVegetation.Add(emptyType, 
            new VegetationBuilder()
                .SetVegetationName("Empty")
                .SetVegetationColour("000000")
                .SetVegetationType(emptyType)
                .Build()
        );
        
        for (byte i = 1; i <= data.Count; i++)
        {
            var typographyData = data[i - 1].Vegetation;
            
            VegetationType newType = new VegetationType(i);
            VegetationDetails newDetails = new VegetationBuilder()
                .SetVegetationName(typographyData.Name)
                .SetVegetationColour(typographyData.Colour)
                .SetVegetationType(newType)
                .Build();

            Console.WriteLine(newDetails.ToString());
            terrainVegetation.Add(newType, newDetails);
        }
        
        return terrainVegetation.ToFrozenDictionary();
    }
    
    private static TerrainType[] DefineTerrain()
    {
        ReadOnlySpan<uint> typographyData = GetImageData(Image.LoadFromFile("Data/Map/Views/typography.bmp"));
        ReadOnlySpan<uint> vegetationData = GetImageData(Image.LoadFromFile("Data/Map/Views/vegetation.bmp"));
        TerrainType[] terrainArray = new TerrainType[MapWidth * MapHeight];
        
        Dictionary<uint, TypographyType> typographyColours = ColourMappingTerrain(TerrainTypography);
        Dictionary<uint, VegetationType> vegetationColours = ColourMappingTerrain(TerrainVegetation);

        Dictionary<uint, TType> ColourMappingTerrain<TType, TDetails>(FrozenDictionary<TType, TDetails> terrainData) where TDetails : ITerrain
        {
            Dictionary<uint, TType> colourMapping = new Dictionary<uint, TType>();
            foreach ((TType terrainKey, TDetails terrainDetails) in terrainData)
            {
                colourMapping.Add(new Color(terrainDetails.Colour).ToUint(), terrainKey);
            }
            return colourMapping;
        }

        for (int i = 0; i < MapWidth * MapHeight; i++)
        {
            if (!typographyColours.TryGetValue(typographyData[i], out TypographyType typographyId)) 
            { 
                throw new ArgumentException("Unknown Typography type: " + typographyData[i] + "Index:" + i);
            }
            if (!vegetationColours.TryGetValue(vegetationData[i], out VegetationType vegetationId))
            {
                throw new ArgumentException("Unknown Vegetation type: " + vegetationData[i] + "Index:" + i);
            }
            terrainArray[i] = new TerrainType(typographyId, vegetationId);
        }
        
        return terrainArray;
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

        for (int index = 0; index < MapWidth * MapHeight; index++)
        {
            ProvinceId pixelId = ProvinceMapData[index];
            ProvinceBuilderDetails details = provinceDetails[pixelId];
            
            int x = index % MapWidth;
            int y = index / MapHeight;
            
            details.AddTerrain(TerrainMapData[index]);
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
        }

        Dictionary<ProvinceId, Vector2I> provinceCentres = [];
        foreach (var (provinceId, details) in provinceDetails) 
        {
            details.GetCentre();
            provinceCentres.Add(provinceId, details.Centre);
        }

        foreach (ProvinceBuilderDetails details in provinceDetails.Values)
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
            var (pixelSize, provinceNeighbours, provinceTerrain, provinceCentre) = newDetails;
            
            IProvince newProvince = builder.SetColour(new Color(provinceData.Colour).ToUint())
                .SetName(provinceData.Name)
                .SetProvinceId(newId)
                .SetSize(pixelSize)
                .SetNeighbours(provinceNeighbours)
                .SetTerrain(provinceTerrain)
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
        ProvinceId[] _provinceArray = new ProvinceId[MapWidth * MapHeight];

        ReadOnlySpan<uint> pixels = GetImageData(provincesImage);
        
        for (int index = 0; index < (MapWidth * MapHeight); index++)
        {
            uint rgb = pixels[index];

            if (ColourMapping.TryGetValue(rgb, out ProvinceId provinceId)) 
            { 
                _provinceArray[index] = provinceId;
            }
        }

        return _provinceArray;
    }
    
    private static ReadOnlySpan<uint> GetImageData(Image image)
    {
        byte[] rawData = image.GetData();
        return MemoryMarshal.Cast<byte, uint>(rawData);
    }
}