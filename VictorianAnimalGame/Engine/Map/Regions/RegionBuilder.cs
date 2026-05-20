using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Regions;

public class RegionBuilder
{
    private string _name;
    private string _colour;
    private List<ProvinceId> _provinces;
    
    public RegionBuilder SetRegionName(string name)
    {
        _name = name;
        return this;
    }
    
    public RegionBuilder SetRegionColour(string colour)
    {
        _colour = colour;
        return this;
    }
    
    public RegionBuilder SetRegionProvinces(List<int> provinces)
    {
        _provinces = [];
        foreach (ushort provinceId in provinces)
        {
            _provinces.Add(new ProvinceId(provinceId));
        }
        return this;
    }
    
    public RegionDetails Build()
    {
        return new RegionDetails(_name, _colour, _provinces);
    }
}