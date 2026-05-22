using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Types;
using VictorianAnimalGame.FileReader.DataReader;
using VictorianAnimalGame.FileReader.DataReader.DataConfig;

namespace VictorianAnimalGame.Engine.Defines;

public static class AnnalsDefines
{
    public static readonly FrozenDictionary<CantonId, CantonDetails> CantonData;
    public static readonly FrozenDictionary<ProvinceId, CantonId> ProvinceCantons;

    static AnnalsDefines()
    {
        CantonData = DefineCantons();
        ProvinceCantons = MapProvinceCantons();
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

    // public static FrozenSet<ProvinceId> GetProvinces(CantonId cantonId)
    // {
    //     return CantonData[cantonId].ProvinceIds;
    // }
    
    public static void Initialize() { }
}