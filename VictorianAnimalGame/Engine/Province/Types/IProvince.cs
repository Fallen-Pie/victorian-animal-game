using System;

namespace VictorianAnimalGame.Engine.Province.Types
{
    public abstract class IProvince
    {
        public ProvinceId Id;
        public uint MapColour;
        public string Name;

        public uint Size;
        public uint Neighbours;

        protected IProvince(ProvinceId newId, uint newMapColour, string newName)
        {
            Id = newId;
            MapColour = newMapColour;
            Name = newName;
        }
    }
}
