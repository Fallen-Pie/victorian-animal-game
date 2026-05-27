using System;
using VictorianAnimalGame.Engine.Components.Property.PropertyCategories;

namespace VictorianAnimalGame.Engine.Components.Property.PropertyTypes;

public class AdminProperty : BaseProperty
{
    protected override PropertyId Id { get; } = new(PropertyCode.Admin);

    public int EmployeeCapacity { get; set; }

    public void ProcessPaperwork() => Console.WriteLine("Admin work in progress...");
    
    public override string ToString() => $"{PropertyCode.Admin}|Name: {Name}|Id: {Id}";
}