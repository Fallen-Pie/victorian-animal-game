using System;
using System.Collections.Frozen;

namespace VictorianAnimalGame.FileReader.MapReader.Strategies;

public class ProvinceAssignmentStrategy : IMapStrategy<int[]>
{
    private readonly FrozenDictionary<uint, int> _colorToProvinceMap;
    private int[] _pixelToProvinceArray;
    private int _width;

    public ProvinceAssignmentStrategy(FrozenDictionary<uint, int> colorToProvinceMap)
    {
        _colorToProvinceMap = colorToProvinceMap;
    }

    public void Initialize(int width, int height)
    {
        _width = width;
        // 1D array representing the 2D grid is best for CPU cache locality
        _pixelToProvinceArray = new int[width * height]; 
        Array.Fill(_pixelToProvinceArray, -1); // -1 represents unassigned/ocean
    }

    public void ProcessPixel(int x, int y, uint colorCode)
    {
        // Ignore the alpha channel to ensure strict RGB matching (0x00FFFFFF masks out the top 8 bits)
        uint rgb = colorCode & 0x00FFFFFF;

        if (_colorToProvinceMap.TryGetValue(rgb, out int provinceId))
        {
            _pixelToProvinceArray[y * _width + x] = provinceId;
        }
    }

    public int[] Complete() => _pixelToProvinceArray;
}