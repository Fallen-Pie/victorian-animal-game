using System;
using Godot.Collections;
using VictorianAnimalGame.FileReader;

namespace VictorianAnimalGame.Scripts.Critters.Species;

public class SpeciesBuilder
{
    private sbyte _adolescentAge;
    private sbyte _adultAge;
    private sbyte _elderAge;
    private sbyte _fertileAge;
    
    private SpeciesType _speciesType;
    private float _foodConsumption;
    private float _workforceValue;
    
    private string _speciesName;
    
    public SpeciesBuilder SetSpeciesName(string name)
    {
        _speciesName = name;
        return this;
    }
    
    public SpeciesBuilder SetAges(AgeStages ages)
    {
        _adolescentAge = ages.Adolescent;
        _adultAge = ages.Adult;
        _elderAge = ages.Elder;    
        return this; // Return the builder instance for chaining
    }

    public SpeciesBuilder SetSpeciesType(SpeciesType type)
    {
        _speciesType = type;
        return this;
    }
    
    public SpeciesBuilder SetPeakFertility(sbyte age)
    {
        _fertileAge = age;
        return this;
    }

    public SpeciesBuilder SetFoodConsumption(float foodConsumption)
    {
         _foodConsumption = foodConsumption;
        return this;
    }
    
    public SpeciesBuilder SetWorkforceValue(float workforceValue)
    {
        _workforceValue = workforceValue;
        return this;
    }
    
    public Species Build()
    {
        return new Species(_speciesType, _foodConsumption, _workforceValue, 
            _adolescentAge, _adultAge, _fertileAge, _elderAge, _speciesName);
    }
}