using System;
using System.Collections.Generic;
using VictorianAnimalGame.Engine.Critters.CritterBuilder.ClassRatio;
using VictorianAnimalGame.Engine.Critters.CritterBuilder.Distribution;
using VictorianAnimalGame.Engine.Critters.Cultures;
using VictorianAnimalGame.Engine.Critters.Species;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Critters.CritterBuilder;

public class CritterBuilder
{
    private ICritterDistribution _critterDistribution;
    private IClassRatio _classRatio;
    private CultureType _culture;
    private SpeciesType _species;
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
    
    public CritterBuilder SetSpecies(SpeciesType newSpeciesDetails)
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
        
        List<int> l = _classRatio.Execute(_totalCount);

        for (int i = 0; i <= 2; i++)
        {
            CritterClass newClass = (CritterClass)i;
            int k = l[i];
            var crit = new CritterEntry(_species, _culture, newClass);
            
            for (short age = 0; age <= maxAge; age++)
            {
                ushort count = (ushort)Math.Round((weights[age] / totalWeight) * k);

                if (count != 0)
                {
                    var newDetails = new CritterDetails(
                        (short)(DateDefines.Year - age),
                        count, 0, 0, 0, 0, 0);
                    crit.Details.Add(newDetails);
                }
            }
            crit.SortDetails();
            h.Add(crit);
        }
        
        return h;
    }
}