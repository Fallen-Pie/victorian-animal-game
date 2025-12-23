using System;
using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Critters.Species;
using VictorianAnimalGame.Scripts.Defines;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.ClassRatio;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.Distribution;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder;

public class ProvinceCritterBuilder
{
    private ICritterDistribution _critterDistribution;
    private IClassRatio _classRatio;
    private CritterCulture _culture;
    private Species _species;
    
    public void SetDistribution(ICritterDistribution newDistribution)
    {
        _critterDistribution = newDistribution;
    }
    
    public void SetRatio(IClassRatio newRatio)
    {
        _classRatio = newRatio;
    }
    
    public void SetSpecies(Species newSpecies)
    {
        _species = newSpecies;
    }
    
    public void SetCulture(CritterCulture newCulture)
    {
        _culture = newCulture;
    }
    
    public LandProvince AddCritterToProvince(uint totalCount, LandProvince province)
    {
        uint maxAge = (uint)(_species.ElderAge * 1.2);
        double[] weights = new double[maxAge + 1];
        double totalWeight = 0;

        for (short age = 0; age <= maxAge; age++)
        {
            double weight = _critterDistribution.Execute(age, _species);
            weights[age] = weight;
            totalWeight += weight;
        }

        for (short age = 0; age <= maxAge; age++)
        {
            uint count = (uint)Math.Round((weights[age] / totalWeight) * totalCount);

            if (count != 0)
            {
                (uint lower, uint middle, uint upper) = _classRatio.Execute(count);
                var newDetails = new CritterDetails(lower, middle, upper);
                province.ProvinceCritters.Add(new CritterEntry((short)(MapDefines.GetCurrentYear() - age), 
                    _species.SpeciesType, _culture, newDetails));
            }
        }

        return province;
    }
}