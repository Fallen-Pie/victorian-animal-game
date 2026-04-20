using VictorianAnimalGame.Engine.Province.Types;

namespace VictorianAnimalGame.Engine.Province.Builder;

public abstract class IProvinceBuilder
{
    protected ProvinceId provinceId;
    protected uint mapColour;
    protected string name;

    public IProvinceBuilder SetProvinceId(ProvinceId newProvinceId)
    {
        provinceId = newProvinceId;
        return this;
    }
    
    public IProvinceBuilder SetColour(uint newMapColour)
    {
        mapColour = newMapColour;
        return this;
    }
    
    public IProvinceBuilder SetName(string newMame)
    {
        name = newMame;
        return this;
    }
    
    public abstract IProvince Build();
}

public class LandProvinceBuilder : IProvinceBuilder
{
    public override LandProvince Build()
    {
        return new LandProvince(provinceId, mapColour, name);
    }
}

public class SeaProvinceBuilder : IProvinceBuilder
{
    public override SeaProvince Build()
    {
        return new SeaProvince(provinceId, mapColour, name);
    }
}

public class WastelandProvinceBuilder : IProvinceBuilder
{
    public override WastelandProvince Build()
    {
        return new WastelandProvince(provinceId, mapColour, name);
    }
}
