namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public struct CritterDataRates
{
    public uint Literate;
    public uint Trained;

    public override string ToString()
    {
        return $"({Literate}.{Trained})";
    }
}