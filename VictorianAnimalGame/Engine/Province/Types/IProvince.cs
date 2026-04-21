using System;

namespace VictorianAnimalGame.Engine.Province.Types;

public abstract class IProvince
{
    public readonly ProvinceId Id;
    public readonly uint MapColour;
    public string Name;

    public uint Size;
    public uint Neighbours;

    protected IProvince(ProvinceId newId, uint newMapColour, string newName)
    {
        Id = newId;
        MapColour = newMapColour;
        Name = newName;
    }

    public override string ToString()
    {
        return $"{GetType()}: {Name}|{Id}|{MapColour}|Size {Size}|Neighbours {Neighbours}";
    }
}