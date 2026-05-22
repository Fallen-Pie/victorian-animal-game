using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Map.Regions;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Types;
using VictorianAnimalGame.FileReader.DataReader;
using VictorianAnimalGame.FileReader.DataReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class AnnalsDefines
{
    public static readonly FrozenDictionary<RegionId, RegionDetails> RegionData;
    public static readonly FrozenDictionary<ProvinceId, RegionId> ProvinceRegions;

    static AnnalsDefines()
    {
        RegionData = DefineRegions();
        ProvinceRegions = MapProvinceRegions();
    }

    private static FrozenDictionary<RegionId, RegionDetails> DefineRegions()
    {
        List<RegionConfig> data = YamlReader.ReadFiles<RegionConfig>("Data/Annals/Regions/");
        Dictionary<RegionId, RegionDetails> mapRegions = [];

        List<ProvinceId> allProvinces = [];
        allProvinces.AddRange(MapDefines.ProvinceData.Keys.Where(id => id.Province is HabitableProvince));

        for (ushort i = 0; i < data.Count; i++)
        {
            var regionData = data[i].Region;
            
            RegionId newRegionId = new RegionId(i);
            RegionDetails newDetails = new RegionBuilder()
                .SetRegionName(regionData.Name)
                .SetRegionColour(regionData.Colour)
                .SetRegionProvinces(regionData.Provinces)
                .Build();
            
            mapRegions.Add(newRegionId, newDetails);

            foreach (ProvinceId provinceId in newDetails.ProvinceIds)
            {
                if (!allProvinces.Remove(provinceId))
                {
                    throw new ArgumentException($"Province {provinceId} is an invalid Province ID");
                }
            }
        }

        foreach (RegionDetails region in mapRegions.Values)
        {
            Console.WriteLine(region);
        }

        if (allProvinces.Count == 0)
        {
            return mapRegions.ToFrozenDictionary();
        }
        throw new ArgumentException($"Unmapped Province IDs: {string.Join(", ", allProvinces)}");
    }
    
    private static FrozenDictionary<ProvinceId, RegionId> MapProvinceRegions()
    {
        Dictionary<ProvinceId, RegionId> mapProvinces = [];
        foreach (var (regionId, details) in RegionData)
        {
            foreach (ProvinceId provinceId in details.ProvinceIds)
            {
                mapProvinces.Add(provinceId, regionId);
            }
        }
        return mapProvinces.ToFrozenDictionary();
    }

    public static FrozenSet<ProvinceId> GetProvinces(RegionId regionId)
    {
        return RegionData[regionId].ProvinceIds;
    }
    
    public static void Initialize() { }
}