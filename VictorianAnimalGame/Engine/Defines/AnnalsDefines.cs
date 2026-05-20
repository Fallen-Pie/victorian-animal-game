using System.Collections.Frozen;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Map.Regions;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.FileReader.DataReader;
using VictorianAnimalGame.FileReader.DataReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class AnnalsDefines
{
    public static readonly FrozenDictionary<ProvinceId, RegionId> ProvinceRegions;
    public static readonly FrozenDictionary<RegionId, RegionDetails> RegionData;

    static AnnalsDefines()
    {
        RegionData = DefineRegions();
    }

    private static FrozenDictionary<RegionId, RegionDetails> DefineRegions()
    {
        List<RegionConfig> data = YamlReader.ReadFiles<RegionConfig>("Data/Annals/Regions/");
        Dictionary<RegionId, RegionDetails> mapRegions = [];
        
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
        }
        
        return mapRegions.ToFrozenDictionary();
    }
}