using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Cantons;

public readonly record struct CantonId(ushort Id)
{
    private CantonDetails CurrentCanton => AnnalsDefines.CantonData[this];
    
    public string Name => CurrentCanton.Name;
    public string Colour => CurrentCanton.Colour;
    public FrozenSet<ProvinceId> ProvinceIds => CurrentCanton.ProvinceIds;

    //public override string ToString() => Id.ToString();
}