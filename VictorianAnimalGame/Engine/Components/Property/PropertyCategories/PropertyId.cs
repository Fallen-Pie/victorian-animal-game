using System;
using VictorianAnimalGame.Engine.Components.Goods.GoodTypes;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyCategories;

public readonly record struct PropertyId(uint Value)
{
    private const int CategoryShift = 3;
    private const int SubCategoryShift = 10;
    private const uint GoodMask = 0x7F;
    private const uint CategoryMask = 0x7;
    
    public PropertyId(PropertyCode categoryCode) : this(0U)
    {
        uint categoryBits = (uint)categoryCode;
        uint sequence = PropertyDefines.GetNextSequence(categoryBits);
        
        // Grab the category code from the type, shift it, and merge
        Value = (sequence << CategoryShift) | categoryBits;
    }
    
    public PropertyId(GoodType goodCode) : this(0U)
    {
        //TODO Change GoodType to return a number like SpeciesType
        uint categoryBits = (goodCode.Value << CategoryShift) | (uint)PropertyCode.Production;
        uint sequence = PropertyDefines.GetNextSequence(categoryBits);
        
        // Move 10 bits for the type, shift it, and merge
        Value = (sequence << SubCategoryShift) | categoryBits;
    }

    public uint RawValue => Value;
    public PropertyCode Category => (PropertyCode)(Value & CategoryMask);
    public uint SequenceValue
    {
        get
        {
            if (Category == PropertyCode.Production)
                return GoodSequenceValue;
            return Value >> CategoryShift;
        }
    }
    public uint Good => (Value >> CategoryShift) & GoodMask;
    private uint GoodSequenceValue => Value >> SubCategoryShift;
    
    public bool Equals(PropertyId other) => Value == other.Value;
    public override int GetHashCode() => (int)Value;

    public string GetRawValue() => $"RawValue({RawValue})";
    public string GetIdDetails()
    {
        if (Category == PropertyCode.Production)
            return $"Category({Category})|Good({Good})|SequenceValue({GoodSequenceValue})"; 
        return $"Category({Category})|SequenceValue({SequenceValue})";
    }
}