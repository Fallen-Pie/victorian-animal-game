using System;
using VictorianAnimalGame.Scripts.Defines;

namespace VictorianAnimalGame.Scripts.Critters.Species;

public readonly struct SpeciesType(ushort speciesId) : IEquatable<SpeciesType>
{
    private ushort SpeciesId { get; } = speciesId;

    public override bool Equals(object obj) => obj is SpeciesType other && Equals(other);
    public bool Equals(SpeciesType other) => SpeciesId == other.SpeciesId;
    public override int GetHashCode() => SpeciesId.GetHashCode();
    public static bool operator ==(SpeciesType left, SpeciesType right) => left.Equals(right);
    public static bool operator !=(SpeciesType left, SpeciesType right) => !(left == right);
    public override string ToString() => CritterDefines.SpeciesNames[this];
}