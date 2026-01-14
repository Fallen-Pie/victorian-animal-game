using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province.Critters.Species;
using VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.ClassRatio;
using VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.Distribution;
using VictorianAnimalGame.Engine.Province.Critters.Cultures;

namespace VictorianAnimalGame.Engine.Province.Critters.CritterBuilder;

public class CritterBuilder
{
    private ICritterDistribution _critterDistribution;
    private IClassRatio _classRatio;
    private CultureType _culture;
    private SpeciesDetails _species;
    private uint _totalCount;


    public CritterBuilder SetDistribution(ICritterDistribution newDistribution)
    {
        _critterDistribution = newDistribution;
        return this;
    }
    
    public CritterBuilder SetRatio(IClassRatio newRatio)
    {
        _classRatio = newRatio;
        return this;
    }
    
    public CritterBuilder SetSpecies(SpeciesDetails newSpeciesDetails)
    {
        _species = newSpeciesDetails;
        return this;
    }
    
    public CritterBuilder SetCulture(CultureType newCulture)
    {
        _culture = newCulture;
        return this;
    }
    
    public CritterBuilder SetAmount(uint newTotalCount)
    {
        _totalCount = newTotalCount;
        return this;
    }
    
    public HashSet<CritterEntry> Build()
    {
        HashSet<CritterEntry> h = [];
        
        uint maxAge = (uint)(_species.ElderAge * 1.2);
        double[] weights = new double[maxAge + 1];
        double totalWeight = 0;

        for (short age = 0; age <= maxAge; age++)
        {
            double weight = _critterDistribution.Execute(age, _species);
            if (weight >= 0)
            {
                weights[age] = weight;
                totalWeight += weight;
            }
        }

        for (short age = 0; age <= maxAge; age++)
        {
            ushort count = (ushort)Math.Round((weights[age] / totalWeight) * _totalCount);

            if (count != 0)
            {
                (ushort lower, ushort middle, ushort upper) = _classRatio.Execute(count);
                var newDetails = new CritterDetails(lower, middle, upper);
                h.Add(new CritterEntry((short)(DateDefines.Year - age), 
                    _species.SpeciesType, _culture, newDetails));
            }
        }

        return h;
    }
}