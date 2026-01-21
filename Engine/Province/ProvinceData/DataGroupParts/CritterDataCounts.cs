namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroupParts;

public struct CritterDataCounts
{
    public uint Dependants;
    public uint Workers;
    public uint Total => Dependants + Workers;

    public override string ToString()
    {
        return $"({Dependants}.{Workers}.{Total})";
    }
}