using System;
using System.Collections.Generic;
using System.Linq;
using VictorianAnimalGame.Engine.Province.Critters;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private HashSet<ProvinceData> ProvinceData { get; set; }
    
    public void GetData(HashSet<CritterEntry> g)
    {
        Span<CritterEntry> critterEntrySpan = [.. g];
        foreach (CritterEntry critter in critterEntrySpan)
        {
            foreach (CritterClass critterClass in Enum.GetValues<CritterClass>())
            {
                ProvinceData newProvinceData = new ProvinceData(
                    critter.GetCritterCulture(),
                    critter.GetCritterAge(),
                    critter.GetCritterSpecies(),
                    critterClass, 
                    critter.GetCritterClassCount(critterClass));
                if (ProvinceData.TryGetValue(newProvinceData, out var currentData))
                {
                    currentData.AddCount(critter.GetCritterClassCount(critterClass));
                }
                else
                {
                    ProvinceData.Add(newProvinceData);
                }
            }
        }

        foreach (ProvinceData critter in ProvinceData)
        {
            Console.WriteLine(critter.ToString());
        }
    }
}