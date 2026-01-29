using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Province.ProvinceData.DataGroup;

namespace VictorianAnimalGame.Engine.Province.ProvinceData;

public class ProvinceManager
{
    private readonly List<CritterData> _critterData = [];
 
    public void GetCritterData(HashSet<CritterEntry> provincialCritterData)
    {
        foreach (CritterEntry critter in provincialCritterData)
        {
            CritterData newCritterData = new CritterData(critter.Species, critter.Culture, critter.Class);
            ReadOnlySpan<CritterDetails> critterSpan = CollectionsMarshal.AsSpan(critter.Details);
            GetFertilityData(critter.Species, critterSpan);
            foreach (CritterDetails currentDetails in critterSpan)
            {
                newCritterData.Dependants += currentDetails.Dependants;
                newCritterData.Workers += currentDetails.Workers;
                newCritterData.Incapacitated += currentDetails.Incapacitated;
                newCritterData.Soldiers += currentDetails.Soldiers;
                newCritterData.Literate += currentDetails.Literate;
                newCritterData.Trained += currentDetails.Trained;
            }
            _critterData.Add(newCritterData);
        }

        foreach (CritterData critter in _critterData)
        {
            Console.WriteLine(critter.ToString());
        }
    }

    public void GetWorkforceData()
    {
        Dictionary<SpeciesType, float> workforceDictionary = [];
        Span<CritterData> critterDataSpan = CollectionsMarshal.AsSpan(_critterData);
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

    public CritterFertility GetFertilityData(SpeciesType speciesType, ReadOnlySpan<CritterDetails> critterSpan)
    {
        int adultIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - speciesType.AdultAge);
        int elderIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - speciesType.ElderAge);
        ReadOnlySpan<CritterDetails> fertileRange = critterSpan[elderIndex..adultIndex];
        Console.WriteLine($"Index {elderIndex} to {adultIndex}|" +
                          $"{fertileRange.Length} fertile range");
        return new CritterFertility();
    }
}