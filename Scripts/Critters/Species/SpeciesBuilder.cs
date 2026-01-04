using System;
using Godot.Collections;
using VictorianAnimalGame.FileReader;

namespace VictorianAnimalGame.Scripts.Critters.Species;

public class SpeciesBuilder
{
    private byte _adolescentAge;
    private byte _adultAge;
    private byte _elderAge;
    private byte _fertileAge;
    
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
        return this;
    }

    public SpeciesBuilder SetSpeciesType(SpeciesType type)
    {
        _speciesType = type;
        return this;
    }
    
    public SpeciesBuilder SetPeakFertility(byte age)
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