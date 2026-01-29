using System;
using System.Runtime.InteropServices;

namespace VictorianAnimalGame.Engine.Extensions;

public static class ConsoleExtensions
{
    public static void GetByteSize<T>() where T : struct
    {
        Console.WriteLine($"Size of {typeof(T)} is {Marshal.SizeOf<T>()}");

    }
}