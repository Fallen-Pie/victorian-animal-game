using System;
using System.Runtime.InteropServices;
using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.FileReader.DataReader;

public static class MapReader
{
    public static ProvinceId[] Scan(Image mapImage)
    {
        int width = mapImage.GetWidth();
        int height = mapImage.GetHeight();
        
        ProvinceId[] _provinceArray = new ProvinceId[width * height];

        // Ensure a consistent 32-bit format (RGBA8)
        if (mapImage.GetFormat() != Image.Format.Rgba8)
        {
            mapImage.Convert(Image.Format.Rgba8);
        }

        // Extract raw data and cast to a span of 32-bit integers for SIMD-friendly, zero-allocation reads
        byte[] rawData = mapImage.GetData();
        ReadOnlySpan<uint> pixels = MemoryMarshal.Cast<byte, uint>(rawData);
        
        for (int index = 0; index < (height * width); index++)
        {
            uint rgb = pixels[index];

            if (MapDefines.ProvinceMapping.TryGetValue(rgb, out ProvinceId provinceId)) 
            { 
                _provinceArray[index] = provinceId;
            }
        }

        return _provinceArray;
    }
}