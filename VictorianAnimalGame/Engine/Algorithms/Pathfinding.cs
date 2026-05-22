using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Map.Regions;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Types;

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
            
            foreach ((ProvinceId edgeId, uint edgeDistance) in current.Neighbours)
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

    public static List<ProvinceId> GetPathLand(
        ProvinceId startProvince, ProvinceId endProvince)
    {
        PriorityQueue<ProvinceId, uint> openSet = new PriorityQueue<ProvinceId, uint>();
        var gScore = new Dictionary<ProvinceId, uint>();
        var cameFrom = new Dictionary<ProvinceId, ProvinceId>();
        
        gScore[startProvince] = 0;
        uint initialFScore = CalculateEuclideanHeuristic(startProvince, endProvince);
        openSet.Enqueue(startProvince, initialFScore);

        while (openSet.Count > 0)
        {
            ProvinceId currentProvince = openSet.Dequeue();

            if (currentProvince == endProvince)
            {
                var path = new List<ProvinceId> { currentProvince };
                while (cameFrom.ContainsKey(currentProvince))
                {
                    currentProvince = cameFrom[currentProvince];
                    path.Insert(0, currentProvince);
                }
                return path;
            }

            foreach ((ProvinceId neighbourProvince, uint distance) in currentProvince.Neighbours)
            {
                if (neighbourProvince.Province is not HabitableProvince) continue;
                uint tentativeGScore = gScore[currentProvince] + distance;

                if (gScore.TryGetValue(neighbourProvince, out uint value) && tentativeGScore >= value) continue;
                cameFrom[neighbourProvince] = currentProvince;
                value = tentativeGScore;
                gScore[neighbourProvince] = value;

                uint fScore = tentativeGScore + CalculateEuclideanHeuristic(neighbourProvince, endProvince);
                openSet.Enqueue(neighbourProvince, fScore);
            }
        }
        
        return [];
    }
    
    private static uint CalculateEuclideanHeuristic(ProvinceId current, ProvinceId goal)
    {
        return (uint)current.Centre.DistanceTo(goal.Centre);
    }
}