using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
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
    public readonly uint MapColour = newMapColour;

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
        string s = $"{GetType().Name[..^8]}:{Name}|{Id}|{MapColour:X}|Size {Size}|Centre {Centre}|Neighbours:";
        foreach (var neighbour in Neighbours)
               s += "\n(" + neighbour.Key.Province.GetSimpleProvince() + "/Dist:" + neighbour.Value + ")";
        return s;
    }
}