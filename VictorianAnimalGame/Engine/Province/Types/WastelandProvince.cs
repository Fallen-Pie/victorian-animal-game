using VictorianAnimalGame.Engine.Province.Builder;

namespace VictorianAnimalGame.Engine.Province.Types;

public class WastelandProvince : IProvince
{
    public WastelandProvince(ProvinceId newId, uint newMapColour, string newName, ProvinceBuilderDetails newDetails) : base(newId, newMapColour, newName, newDetails)
    {
        
    }
}