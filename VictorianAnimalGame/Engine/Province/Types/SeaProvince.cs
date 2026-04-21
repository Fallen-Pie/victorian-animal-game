using System;
using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public class SeaProvince : IProvince
{
    public SeaProvince(ProvinceId newId, uint newMapColour, string newName, ProvinceBuilderDetails newDetails) : base(newId, newMapColour, newName, newDetails)
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