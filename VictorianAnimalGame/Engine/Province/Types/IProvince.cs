using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Map.Terrain;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public abstract class IProvince(
    ProvinceId newId,
    uint newMapColour,
    string newName,
    uint pixelSize,
    Dictionary<ProvinceId, uint> provinceNeighbours,
    Dictionary<TerrainType, uint> provinceTerrain,
    Vector2I provinceCentre)
{
    public readonly ProvinceId Id = newId;
    public readonly Color MapColour = newMapColour.ToColour();

    public readonly uint Size = pixelSize;
    public readonly FrozenDictionary<ProvinceId, uint> Neighbours = provinceNeighbours.ToFrozenDictionary();
    public readonly Vector2I Centre = provinceCentre;
    
    public string Name = newName;
    public Dictionary<TerrainType, uint> Terrain = provinceTerrain;

    protected abstract bool Traversable { get; }
    protected abstract bool Communications { get; }


    public string GetSimpleProvince()
    {
        return $"{GetType().Name[..^8]}:{Name}|{Id}";
    }

    public override string ToString()
    {
        string provinceString = $"{GetType().Name[..^8]}:{Name}|{Id}|{MapColour.ToHtml(false)}|Size {Size}|Centre {Centre}\nTerrain:(";
        uint total = 0;
        foreach (var terrain in Terrain)
        {
            provinceString += $"{terrain.Key}:{terrain.Value}/";
            total += terrain.Value;
        }
        provinceString += $"Terrain Count:{total})\nNeighbours:(";
        foreach (var neighbour in Neighbours)
               provinceString += neighbour.Key.Province.GetSimpleProvince() + "/Dist:" + neighbour.Value + "|";
        return provinceString.Remove(provinceString.Length - 1, 1) + ")";
    }
}