using System;
using VictorianAnimalGame.Engine.Components.Goods.GoodTypes;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyCategories;

public readonly record struct PropertyId
{
    private readonly uint _value;
    
    private const int CategoryShift = 3;
    private const int SubCategoryShift = 10;
    private const uint GoodMask = 0x7F;
    private const uint CategoryMask = 0x7;

    // Constructor 1: You only need to provide the unique ID. 
    public PropertyId(PropertyCode categoryCode)
    {
        uint categoryBits = (uint)categoryCode;
        
        uint sequence = PropertyIdRegistry.GetNextSequence(categoryBits);
        
        // Grab the category code from the type, shift it, and merge
        _value = (sequence << CategoryShift) | categoryBits;
    }
    
    // Constructor 2: You only need to provide the unique ID and GoodType. 
    public PropertyId(PropertyCode categoryCode, GoodType goodCode)
    {
        //TODO Change GoodType to return a number like SpeciesType
        uint categoryBits = (goodCode.RawValue << CategoryShift) | (uint)categoryCode;
        
        uint sequence = PropertyIdRegistry.GetNextSequence(categoryBits);
        
        // Move 10 bits for the type, shift it, and merge
        _value = (sequence << SubCategoryShift) | categoryBits;
    }

    public uint RawValue => _value;
    public PropertyCode Category => (PropertyCode)(_value & CategoryMask);
    public uint SequenceValue => _value >> CategoryShift;
    public uint Good => (_value >> CategoryShift) & GoodMask;
    public uint GoodSequenceValue => _value >> SubCategoryShift;
    
    public bool Equals(PropertyId other) => _value == other._value;
    public override int GetHashCode() => _value.GetHashCode();

    public string GetRawValue() => $"RawValue({RawValue})";
    public string GetIdDetails() => $"Category({Category})|SequenceValue({SequenceValue})";
    public string GetIdDetails(GoodType goodCode)
    {
       return $"Category({Category})|Good({Good})|SequenceValue({GoodSequenceValue})"; 
    } 
}