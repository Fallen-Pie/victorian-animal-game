using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map.Cantons;

public readonly record struct CantonId(ushort cantonId)
{
    private ushort Id { get; } = cantonId;
    public string Name => AnnalsDefines.CantonData[this].Name;
    public string Colour => AnnalsDefines.CantonData[this].Colour;
    public FrozenSet<ProvinceId> ProvinceIds => AnnalsDefines.CantonData[this].ProvinceIds;

    public bool Equals(CantonId other) => Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
    public override string ToString() => Id.ToString();
}