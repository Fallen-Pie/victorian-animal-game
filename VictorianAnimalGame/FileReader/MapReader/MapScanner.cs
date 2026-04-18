using System;
using System.Runtime.InteropServices;
using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.FileReader.MapReader;

public static class MapScanner
{
    public static ProvinceType[] Scan(Image mapImage)
    {
        int width = mapImage.GetWidth();
        int height = mapImage.GetHeight();
        
        ProvinceType[] _provinceArray = new ProvinceType[width * height];

        // Ensure a consistent 32-bit format (RGBA8)
        if (mapImage.GetFormat() != Image.Format.Rgba8)
        {
            mapImage.Convert(Image.Format.Rgba8);
        }

        // Extract raw data and cast to a span of 32-bit integers for SIMD-friendly, zero-allocation reads
        byte[] rawData = mapImage.GetData();
        ReadOnlySpan<uint> pixels = MemoryMarshal.Cast<byte, uint>(rawData);
        
        int index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                uint rgb = pixels[index++] & 0x00FFFFFF;
                
                if (MapDefines.ProvinceMapping.TryGetValue(rgb, out ProvinceType provinceId))
                {
                    _provinceArray[y * width + x] = provinceId;
                }
            }
        }

        return _provinceArray;
    }
    
    // Helper to convert standard Hex/Color to the little-endian uint format Godot's Rgba8 produces
    public static uint ColorToMapUint(Color color)
    {
        return ((uint)color.A8 << 24) | ((uint)color.B8 << 16) | ((uint)color.G8 << 8) | (uint)color.R8;
    }
}