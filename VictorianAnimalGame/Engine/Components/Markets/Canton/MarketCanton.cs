using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Algorithms;
using VictorianAnimalGame.Engine.Map.Regions;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Components.Markets.Canton;

public class MarketCanton
{
    public RegionId MarketRegion { get; }
    public ProvinceId CentreProvince { get; set; }
    public Dictionary<ProvinceId, uint> DistanceToCentre { get; set; }

    public MarketCanton(RegionId newMarketRegion, ProvinceId newCentreProvince)
    {
        MarketRegion = newMarketRegion;
        CentreProvince = newCentreProvince;
        GetDistances();
    }

    public void ChangeCentreProvince(ProvinceId newCentreProvince)
    {
        if (!MarketRegion.ProvinceIds.Contains(newCentreProvince))
            throw new ArgumentException($"Cannot change centre province to {newCentreProvince.Name}. " +
                                        $"Province is not located in {MarketRegion.Name}");
        CentreProvince = newCentreProvince;
        GetDistances();
    }

    private void GetDistances()
    {
        DistanceToCentre = Pathfinding.RegionGetShortestPath(MarketRegion, CentreProvince);
    }
}