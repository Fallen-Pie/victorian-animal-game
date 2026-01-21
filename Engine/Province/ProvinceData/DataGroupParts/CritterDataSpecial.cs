namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroupParts;

public struct CritterDataSpecial
{
    public uint Students;
    public uint Trained;
    public uint Indisposed;

    public override string ToString()
    {
        return $"({Students}.{Trained}.{Indisposed})";
    }
}