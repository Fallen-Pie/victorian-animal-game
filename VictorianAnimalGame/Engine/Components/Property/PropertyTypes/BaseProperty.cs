using VictorianAnimalGame.Engine.Components.Property.PropertyCategories;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyTypes;

public abstract class BaseProperty
{
    protected abstract PropertyId Id { get; }
    protected string Name { get; set; } = string.Empty;
    protected string Location { get; init; } = string.Empty;

    public void SetNewName(string name) => Name = name;

    public override int GetHashCode() => (int)Id.RawValue;
}