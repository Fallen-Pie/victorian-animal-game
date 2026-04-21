using System.Collections.Generic;
using Godot;

namespace VictorianAnimalGame.Engine.Province.Builder;

public class ProvinceBuilderDetails
{
    public uint PixelSize;
    public Vector2I Centre;
    public Dictionary<ProvinceId, uint> ProvinceNeighbours = [];
    
    long sumXPixels;
    long sumYPixels;

    public void AddPixel(int x, int y)
    {
        PixelSize++;
        sumXPixels += x;
        sumYPixels += y;
    }
    
    public void GetCentre()
    {
        long centroidX = sumXPixels / PixelSize;
        long centroidY = sumYPixels / PixelSize;
        Centre = new Vector2I((int)centroidX, (int)centroidY);
    }
    
    public void AddNeighbours(ProvinceId neighbour)
    {
        ProvinceNeighbours.TryAdd(neighbour, 0);
    }
    
    public void GetNeighboursDistance(ref Dictionary<ProvinceId, Vector2I> provinceCentres)
    {
        foreach (var province in ProvinceNeighbours.Keys)
        {
            uint distance = (uint)Centre.DistanceTo(provinceCentres[province]);
            ProvinceNeighbours[province] = distance;
        }
    }

    public void Deconstruct(out uint size, out Dictionary<ProvinceId, uint> neighbours, out Vector2I centre)
    {
        size = PixelSize;
        neighbours = ProvinceNeighbours;
        centre = Centre;
    }
    

}