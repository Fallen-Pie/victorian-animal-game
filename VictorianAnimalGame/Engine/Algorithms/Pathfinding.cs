using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Map.Regions;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Algorithms;

public static class Pathfinding
{
    public static Dictionary<ProvinceId, uint> RegionGetShortestPath(
        RegionId region, ProvinceId startProvince)
    {
        Dictionary<ProvinceId, uint> distances = [];
        PriorityQueue<ProvinceId, uint> priorityQueue = new PriorityQueue<ProvinceId, uint>();

        foreach (ProvinceId province in region.ProvinceIds)
        {
            distances[province] = uint.MaxValue;
        }
        distances[startProvince] = 0;
        priorityQueue.Enqueue(startProvince, 0);

        while (priorityQueue.Count > 0)
        {
            priorityQueue.TryDequeue(out ProvinceId current, out uint currentDist);

            if (currentDist > distances[current]) continue;
            if (!region.ProvinceIds.Contains(current)) continue;
            
            foreach (var (edgeId, edgeDistance) in current.Neighbours)
            {
                if (!distances.ContainsKey(edgeId)) continue;
                
                uint newDist = distances[current] + edgeDistance;
                if (newDist < distances[edgeId])
                {
                    distances[edgeId] = newDist;
                    priorityQueue.Enqueue(edgeId, newDist);
                }
            }
        }
        return distances;
    }
}