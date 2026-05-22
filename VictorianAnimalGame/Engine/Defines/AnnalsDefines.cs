using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Map.Regions;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Types;
using VictorianAnimalGame.FileReader.DataReader;
using VictorianAnimalGame.FileReader.DataReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class AnnalsDefines
{
    public static readonly FrozenDictionary<CantonId, CantonDetails> CantonData;
    public static readonly FrozenDictionary<ProvinceId, CantonId> ProvinceCantons;
    public static readonly FrozenDictionary<RegionId, RegionDetails> RegionData;
    public static readonly FrozenDictionary<CantonId, RegionId> CantonRegions;


    static AnnalsDefines()
    {
        CantonData = DefineCantons();
        ProvinceCantons = MapProvinceCantons();
        RegionData = DefineRegions();
        CantonRegions = MapCantonRegions();
    }

    private static FrozenDictionary<CantonId, CantonDetails> DefineCantons()
    {
        List<CantonConfig> data = YamlReader.ReadFiles<CantonConfig>("Data/Annals/Cantons/");
        Dictionary<CantonId, CantonDetails> mapCantons = [];

        List<ProvinceId> allProvinces = [];
        allProvinces.AddRange(MapDefines.ProvinceData.Keys.Where(id => id.Province is HabitableProvince));

        for (ushort i = 0; i < data.Count; i++)
        {
            var regionData = data[i].Canton;
            
            CantonId newCantonId = new CantonId(i);
            CantonDetails newDetails = new CantonBuilder()
                .SetRegionName(regionData.Name)
                .SetRegionColour(regionData.Colour)
                .SetRegionProvinces(regionData.Provinces)
                .Build();
            
            mapCantons.Add(newCantonId, newDetails);

            foreach (ProvinceId provinceId in newDetails.ProvinceIds)
            {
                if (!allProvinces.Remove(provinceId))
                {
                    throw new ArgumentException($"Province {provinceId} is an invalid Province ID");
                }
            }
        }

        foreach (CantonDetails canton in mapCantons.Values)
        {
            Console.WriteLine(canton);
        }

        if (allProvinces.Count == 0)
        {
            return mapCantons.ToFrozenDictionary();
        }
        throw new ArgumentException($"Unmapped Province IDs: {string.Join(", ", allProvinces)}");
    }
    
    private static FrozenDictionary<ProvinceId, CantonId> MapProvinceCantons()
    {
        Dictionary<ProvinceId, CantonId> mapProvinces = [];
        foreach (var (cantonId, details) in CantonData)
        {
            foreach (ProvinceId provinceId in details.ProvinceIds)
            {
                mapProvinces.Add(provinceId, cantonId);
            }
        }
        return mapProvinces.ToFrozenDictionary();
    }
    
    private static FrozenDictionary<RegionId, RegionDetails> DefineRegions()
    {
        List<RegionConfig> data = YamlReader.ReadFiles<RegionConfig>("Data/Annals/Regions/");
        Dictionary<RegionId, RegionDetails> mapRegions = [];
        
        Dictionary<string, CantonId> cantonNames = [];
        foreach (CantonId canton in CantonData.Keys)
            cantonNames.Add(canton.Name, canton);

        List<CantonId> allCantons = [];
        allCantons.AddRange(CantonData.Keys);

        for (ushort i = 0; i < data.Count; i++)
        {
            var regionData = data[i].Region;
            
            RegionId newCantonId = new RegionId(i);
            RegionDetails newDetails = new RegionBuilder()
                .SetRegionName(regionData.Name)
                .SetRegionColour(regionData.Colour)
                .SetRegionCantons(regionData.Cantons, cantonNames)
                .Build();
            
            mapRegions.Add(newCantonId, newDetails);

            foreach (CantonId cantonId in newDetails.CantonIds)
            {
                if (!allCantons.Remove(cantonId))
                {
                    throw new ArgumentException($"Canton {cantonId} is an invalid Canton ID");
                }
            }
        }
        
        foreach (RegionDetails region in mapRegions.Values)
        {
            Console.WriteLine(region);
        }
        
        if (allCantons.Count == 0)
        {
            return mapRegions.ToFrozenDictionary();
        }
        throw new ArgumentException($"Unmapped Canton IDs: {string.Join(", ", allCantons)}");
    }
    
    private static FrozenDictionary<CantonId, RegionId> MapCantonRegions()
    {
        Dictionary<CantonId, RegionId> mapCantons = [];
        foreach (var (regionId, details) in RegionData)
        {
            foreach (CantonId cantonId in details.CantonIds)
            {
                mapCantons.Add(cantonId, regionId);
            }
        }
        return mapCantons.ToFrozenDictionary();
    }

    public static RegionId GetProvinceRegion(ProvinceId province)
    {
        return CantonRegions[ProvinceCantons[province]];
    }
    
    public static void Initialize() { }
}