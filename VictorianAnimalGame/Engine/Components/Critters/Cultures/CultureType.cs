using System;

namespace VictorianAnimalGame.Engine.Components.Critters.Cultures;

public readonly record struct CultureType(ushort CultureId)
{
    //private ushort CultureId { get; } = cultureId;

    public override string ToString() => CultureId.ToString();
}