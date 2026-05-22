using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Cantons;

public class CantonBuilder
{
    private string _name;
    private string _colour;
    private HashSet<ProvinceId> _provinces;
    
    public CantonBuilder SetRegionName(string name)
    {
        _name = name;
        return this;
    }
    
    public CantonBuilder SetRegionColour(string colour)
    {
        _colour = colour;
        return this;
    }
    
    public CantonBuilder SetRegionProvinces(List<int> provinces)
    {
        _provinces = [];
        foreach (ushort provinceId in provinces)
        {
            _provinces.Add(new ProvinceId(provinceId));
        }
        return this;
    }
    
    public CantonDetails Build()
    {
        return new CantonDetails(_name, _colour, _provinces);
    }
}