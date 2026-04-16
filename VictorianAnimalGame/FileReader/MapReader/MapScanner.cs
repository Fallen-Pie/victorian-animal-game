using System;
using System.Runtime.InteropServices;
using Godot;

namespace VictorianAnimalGame.FileReader.MapReader;

public static class MapScanner
{
    public static TResult Scan<TResult>(Image mapImage, IMapStrategy<TResult> strategy)
    {
        int width = mapImage.GetWidth();
        int height = mapImage.GetHeight();

        // Ensure a consistent 32-bit format (RGBA8)
        if (mapImage.GetFormat() != Image.Format.Rgba8)
        {
            mapImage.Convert(Image.Format.Rgba8);
        }

        // Extract raw data and cast to a span of 32-bit integers for SIMD-friendly, zero-allocation reads
        byte[] rawData = mapImage.GetData();
        ReadOnlySpan<uint> pixels = MemoryMarshal.Cast<byte, uint>(rawData);

        strategy.Initialize(width, height);

        int index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                strategy.ProcessPixel(x, y, pixels[index++]);
            }
        }

        return strategy.Complete();
    }
    
    // Helper to convert standard Hex/Color to the little-endian uint format Godot's Rgba8 produces
    public static uint ColorToMapUint(Color color)
    {
        return ((uint)color.A8 << 24) | ((uint)color.B8 << 16) | ((uint)color.G8 << 8) | (uint)color.R8;
    }
}