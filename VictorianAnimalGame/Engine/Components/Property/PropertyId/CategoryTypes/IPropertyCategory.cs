namespace VictorianAnimalGame.Engine.Components.Property.PropertyId.CategoryTypes;

public interface IPropertyCategory
{
    // C# 11 feature: Interfaces can have static abstract members
    static abstract uint CategoryCode { get; }
}

// Our "Phantom Types" - empty, zero-byte structs