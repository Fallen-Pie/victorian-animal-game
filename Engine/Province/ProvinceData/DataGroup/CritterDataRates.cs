namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public struct CritterDataRates
{
    public uint Students;
    public uint Trained;

    public override string ToString()
    {
        return $"({Students}.{Trained})";
    }
}