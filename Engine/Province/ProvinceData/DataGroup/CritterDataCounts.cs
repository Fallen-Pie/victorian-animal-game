namespace VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

public struct CritterDataCounts
{
    public uint Dependants;
    public uint Workers;
    public uint Incapacitated;
    public uint Soldiers;
    
    public uint Total => Dependants + Workers + Incapacitated + Soldiers;

    public override string ToString()
    {
        return $"({Dependants}.{Workers}.{Incapacitated}.{Soldiers}/{Total})";
    }
}