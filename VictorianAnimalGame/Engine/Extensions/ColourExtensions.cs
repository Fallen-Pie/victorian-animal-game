using Godot;

namespace VictorianAnimalGame.Engine.Extensions;

public static class ColourExtensions
{
    public static uint ToUint(this Color color)
    {
        return ((uint)color.A8 << 24) | ((uint)color.B8 << 16) | ((uint)color.G8 << 8) | (uint)color.R8;
    }
    
    public static Color ToColour(this uint colour)
    {
        byte a = (byte)((colour >> 24) & 0xFF);
        byte b = (byte)((colour >> 16) & 0xFF);
        byte g = (byte)((colour >> 8) & 0xFF);
        byte r = (byte)(colour & 0xFF);
        return Color.Color8(r, g, b, a);
    }
}