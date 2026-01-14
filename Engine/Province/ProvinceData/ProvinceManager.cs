using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province.Critters;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private HashSet<CritterData> ProvinceData = [];
    
    public void GetData(HashSet<CritterEntry> g)
    {
        Span<CritterEntry> critterEntrySpan = [.. g];
        foreach (CritterEntry critter in critterEntrySpan)
        {
            foreach (CritterClass critterClass in Enum.GetValues<CritterClass>())
            {
                CritterData newCritterData = new CritterData(
                    critter.GetCritterCulture(),
                    critter.GetCritterAge(),
                    critter.GetCritterSpecies(),
                    critterClass, 
                    critter.GetCritterClassCount(critterClass));
                if (ProvinceData.TryGetValue(newCritterData, out var currentData))
                {
                    currentData.Count += critter.GetCritterClassCount(critterClass);
                    ProvinceData.Remove(newCritterData);
                    ProvinceData.Add(currentData);
                }
                else
                {
                    ProvinceData.Add(newCritterData);
                }
            }
        }

        foreach (CritterData critter in ProvinceData)
        {
            Console.WriteLine(critter.ToString());
        }
    }
}