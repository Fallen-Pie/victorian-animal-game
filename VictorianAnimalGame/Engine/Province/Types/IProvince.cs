using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public abstract class IProvince
{
    public readonly ProvinceId Id;
    public readonly uint MapColour;

    public readonly uint Size;
    public readonly FrozenDictionary<ProvinceId, uint> Neighbours;
    public readonly Vector2I Centre;
    
    public string Name;

    protected IProvince(ProvinceId newId, uint newMapColour, string newName, uint pixelSize, 
        Dictionary<ProvinceId, uint> provinceNeighbours, Vector2I provinceCentre)
    {
        Id = newId;
        MapColour = newMapColour;
        Name = newName;
        Size = pixelSize;
        Neighbours = provinceNeighbours.ToFrozenDictionary();
        Centre = provinceCentre;
    }

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