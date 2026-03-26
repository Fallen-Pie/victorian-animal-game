using System;
using VictorianAnimalGame.Engine.Components.Property.PropertyId.CategoryTypes;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyId;

public readonly record struct PropertyId<TCategory>
    where TCategory : struct, IPropertyCategory // Constrain to our marker types
{
    private readonly uint _value;
    private const int CategoryShift = 3;
    private const uint UniqueIdMask = 0x1FFFFFFF;
    private const uint CategoryMask = 0x7;

    // Constructor: You only need to provide the unique ID. 
    // The category is pulled automatically from the generic type parameter!
    public PropertyId(uint uniqueId)
    {
        if (uniqueId > UniqueIdMask)
            throw new ArgumentOutOfRangeException(nameof(uniqueId), "ID exceeds 29 bits.");

        // Grab the category code from the type, shift it, and merge
        _value = (uniqueId << CategoryShift) | TCategory.CategoryCode;
    }

    public uint RawValue => _value;
    public uint UniqueId => _value & CategoryMask;

    public bool Equals(PropertyId<TCategory> other) => _value == other._value;
    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => $"{typeof(TCategory).Name.Replace("Category", "")} ({UniqueId})";
}