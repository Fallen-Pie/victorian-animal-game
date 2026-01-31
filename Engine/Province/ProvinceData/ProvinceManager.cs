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

    public CritterBirthRate GetFertilityData(SpeciesType speciesType, ReadOnlySpan<CritterDetails> critterSpan)
    {
        int adultIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - speciesType.AdultAge + 1);
        int elderIndex = critterSpan.BinarySearchByYearValue(DateDefines.Year - speciesType.ElderAge);
        ReadOnlySpan<CritterDetails> fertileRange = critterSpan[elderIndex..adultIndex];
        Dictionary<int, double> fertilityValues = GetFertilityByYear(speciesType);
        // string st = "";
        // foreach (var year in d)
        // {
        //     st += $"{DateDefines.Year - year.Key}:{year.Value}/";
        // }
        // Console.WriteLine(st);
        // string s = $"Index {elderIndex} to {adultIndex}|";
        // foreach (var critter in fertileRange)
        // {
        //     s += $"{critter.Year}/";
        // }
        // Console.WriteLine(s);
        return new CritterBirthRate();
    }

    public Dictionary<int, double> GetFertilityByYear(SpeciesType speciesType)
    {
        Dictionary<int, double> fertilityByYear = GenerateIntegerCurve(speciesType.AdultAge, speciesType.FertileAge, speciesType.ElderAge);
        // for (ushort year = speciesType.AdultAge; year <= speciesType.ElderAge; year++)
        // {
        //     fertilityByYear.Add(year, 1.0f);
        // }
        foreach (var d in fertilityByYear)
        {
            Console.WriteLine($"Age:{d.Key}, Weight:{d.Value}");
        }
        return fertilityByYear;
    }
    
    public Dictionary<int, double> GenerateIntegerCurve(
        int startX, 
        int peakX, 
        int endX, 
        double steepness = 1.0, 
        double maxHeight = 1.0)
    {
        var results = new Dictionary<int, double>();

        // Validation: Start < Peak < End
        if (startX >= peakX || peakX >= endX)
        {
            throw new ArgumentException("Constraints violated: Start must be < Peak and Peak must be < End.");
        }

        startX--;
        endX++;
        
        // Shift coordinates to relative space
        double L = endX - startX; // Relative End
        double P = peakX - startX; // Relative Peak
        double k = steepness;
        double n = (k * (L - P)) / P;

        // Normalization factor based on the relative peak
        double normalization = Math.Pow(P, k) * Math.Pow(L - P, n);

        for (int x = startX; x <= endX; x++)
        {
            if (x == startX || x == endX)
            {
                results[x] = 0.0;
                continue;
            }

            // Calculate relative x
            double relX = x - startX;
            double raw = Math.Pow(relX, k) * Math.Pow(L - relX, n);
        
            results[x] = (raw / normalization) * maxHeight;
        }

        results.Remove(startX);
        results.Remove(endX);
        return results;
    }
}