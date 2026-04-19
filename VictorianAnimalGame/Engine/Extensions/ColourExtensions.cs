using Godot;

namespace VictorianAnimalGame.Engine.Extensions;

public static class ColourExtensions
{
    public static uint ToUint(this Color color)
    {
        return ((uint)color.A8 << 24) | ((uint)color.B8 << 16) | ((uint)color.G8 << 8) | (uint)color.R8;
    }
}