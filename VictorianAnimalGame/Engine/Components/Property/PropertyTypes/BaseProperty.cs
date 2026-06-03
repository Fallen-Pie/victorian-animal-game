using VictorianAnimalGame.Engine.Components.Property.PropertyCategories;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyTypes;

public abstract class BaseProperty
{
    protected abstract PropertyId Id { get; }
    protected string Name { get; set; } = string.Empty;
    protected ProvinceId Location { get; init; }
    //protected Zone ZoneType { get; init; }

    public void SetNewName(string name) => Name = name;

    public override int GetHashCode() => (int)Id.RawValue;
}