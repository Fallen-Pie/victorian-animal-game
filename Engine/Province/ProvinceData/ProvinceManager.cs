using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private CritterData[] CritterData { get; set; }
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        HashSet<CritterData> newCritterArray = [];
        Span<CritterEntry> critterEntrySpan = [.. provincialCritterData];
        foreach (CritterEntry critter in critterEntrySpan)
        {
            foreach (CritterClass currentClass in Enum.GetValues<CritterClass>())
            {
                CritterData newData = new CritterData(
                    critter.Species, critter.Culture, currentClass);
                if (newCritterArray.TryGetValue(newData, out var updatedData))
                {
                    newCritterArray.Remove(newData);
                }
                else
                {
                    updatedData = newData;
                }

                // switch (critter.LifeStage)
                // {
                //     case CritterLifeStage.Young:
                //         updatedData.Dependants += critter.GetCritterClassCount(currentClass);
                //         break;
                //     default:
                //         updatedData.Workers += critter.GetCritterClassCount(currentClass);
                //         break;
                // }
                newCritterArray.Add(updatedData);
            }
        }

        CritterData = [.. newCritterArray];

        foreach (CritterData critter in CritterData)
        {
            Console.WriteLine(critter.ToString());
        }
    }

    public void GetWorkforceData()
    {
        Dictionary<SpeciesType, float> workforceDictionary = [];
        Span<CritterData> critterDataSpan = CritterData;
        foreach (CritterData critterGroup in critterDataSpan)
        {
            (SpeciesType species, float workforceValue) = critterGroup.GetWorkforce();
            ref float existingWorkforce = 
                ref CollectionsMarshal.GetValueRefOrAddDefault(workforceDictionary, species, out bool _);
            existingWorkforce = MathF.Round(existingWorkforce + workforceValue, 2);
        }
        
        foreach (var workforce in workforceDictionary)
        {
            Console.WriteLine($"{workforce.Key.Name} workforce: {workforce.Value}");;
        }
    }
}