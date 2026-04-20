using System;

namespace VictorianAnimalGame.Engine.Province.Types;

public class SeaProvince : IProvince
{
    public SeaProvince(ProvinceId newId, uint newMapColour, string newName) : base(newId, newMapColour, newName)
    {
        
    }

    public string GetDetails()
    {
        throw new NotImplementedException();
    }

    public void SetName()
    {
        throw new NotImplementedException();
    }
}