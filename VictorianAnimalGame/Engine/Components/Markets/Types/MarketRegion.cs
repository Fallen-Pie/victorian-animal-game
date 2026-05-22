using System.Collections.Generic;
using VictorianAnimalGame.Engine.Algorithms;
using VictorianAnimalGame.Engine.Map.Cantons;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Components.Markets.Types;

public class MarketRegion : MarketCanton
{
    public Dictionary<MarketCanton, uint> MarketCantons { get; }
    
    public MarketRegion(CantonId newCanton, ProvinceId newCentre, List<MarketCanton> marketCantons) : base(newCanton, newCentre)
    {
        Dictionary<MarketCanton, uint> cantonDistance = [];
        foreach (MarketCanton canton in marketCantons)
            cantonDistance[canton] = Pathfinding.GetLandDistance(CentreProvince, canton.CentreProvince);
        MarketCantons = cantonDistance;
    }

    
}