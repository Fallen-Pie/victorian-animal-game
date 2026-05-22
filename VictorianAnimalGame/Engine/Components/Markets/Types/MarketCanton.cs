using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Algorithms;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Components.Markets.Types;

public class MarketCanton
{
    public CantonId Canton { get; }
    public ProvinceId CentreProvince { get; set; }
    public Dictionary<ProvinceId, uint> DistanceToCentre { get; set; }

    public MarketCanton(CantonId newCanton, ProvinceId newCentre)
    {
        Canton = newCanton;
        CentreProvince = newCentre;
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
        Dictionary<ProvinceId, uint> provinceDistances = Pathfinding.RegionGetShortestPath(Canton, CentreProvince);
        provinceDistances.Remove(CentreProvince);
        DistanceToCentre = provinceDistances;
    }
}