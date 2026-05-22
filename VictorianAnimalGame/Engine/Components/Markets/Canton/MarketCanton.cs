using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Algorithms;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Components.Markets.Canton;

public class MarketCanton
{
    public CantonId Canton { get; }
    public ProvinceId CentreProvince { get; set; }
    public Dictionary<ProvinceId, uint> DistanceToCentre { get; set; }

    public MarketCanton(CantonId newMarketCanton, ProvinceId newCentreProvince)
    {
        Canton = newMarketCanton;
        CentreProvince = newCentreProvince;
        GetDistances();
    }

    public void ChangeCentreProvince(ProvinceId newCentreProvince)
    {
        if (!Canton.ProvinceIds.Contains(newCentreProvince))
            throw new ArgumentException($"Cannot change centre province to {newCentreProvince.Name}. " +
                                        $"Province is not located in {Canton.Name}");
        CentreProvince = newCentreProvince;
        GetDistances();
    }

    private void GetDistances()
    {
        DistanceToCentre = Pathfinding.RegionGetShortestPath(Canton, CentreProvince);
    }
}