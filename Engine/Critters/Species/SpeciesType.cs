using System;
using System.Collections.Frozen;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Critters.Species;

public readonly struct SpeciesType(ushort speciesId) : IEquatable<SpeciesType>
{
    private ushort SpeciesId { get; } = speciesId;
    
    public string Name => CritterDefines.Species[this].SpeciesName;
    public float Workforce => CritterDefines.Species[this].WorkforceValue;
    
    public int AdultAge => CritterDefines.Species[this].AdultAge;
    public int FertileAge => CritterDefines.Species[this].FertileAge;
    public int ElderAge => CritterDefines.Species[this].ElderAge;
    
    public FrozenDictionary<int, float> BirthDistribution => CritterDefines.Species[this].FertilityByAge;

    public override bool Equals(object obj) => obj is SpeciesType other && Equals(other);
    public bool Equals(SpeciesType other) => SpeciesId == other.SpeciesId;
    public override int GetHashCode() => SpeciesId.GetHashCode();
    public static bool operator ==(SpeciesType left, SpeciesType right) => left.Equals(right);
    public static bool operator !=(SpeciesType left, SpeciesType right) => !(left == right);
    public override string ToString() => SpeciesId.ToString();


}