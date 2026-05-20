using System;
using System.Collections.Generic;
using Godot;
using Microsoft.VisualBasic.FileIO;
using VictorianAnimalGame.Engine.Extensions;
using VictorianAnimalGame.Engine.Map.Terrain;
using VictorianAnimalGame.Engine.Map.Terrain.Rivers;
using VictorianAnimalGame.Engine.Map.Terrain.Typography;
using VictorianAnimalGame.Engine.Map.Terrain.Vegetation;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Defines.MapDetails;

public class RiverCreation
{
    private readonly uint baseRiver = new Color("09477fff").ToUint();
    private readonly uint noRiver = new Color("ffffffff").ToUint();
    
    private readonly uint startRiver = new Color("f52738ff").ToUint();
    private readonly uint mergeRiver = new Color("54d43bff").ToUint();
    private readonly uint splitRiver = new Color("f59e27ff").ToUint();
    private readonly uint deltaRiver = new Color("12d5b0ff").ToUint();
    private readonly uint endRiver = new Color("9f27ceff").ToUint();

    private Dictionary<TypographyType, ITerrain> _rivers = [];
    private TerrainType[] _terrainMaps;

    public RiverCreation(TerrainType[] terrainMap)
    {
        _terrainMaps = terrainMap;
    }
    
    public Dictionary<TypographyType, ITerrain> GetRiverData(int typographyCount)
    {
        ReadOnlySpan<uint> riverData = MapDefines.GetImageData(Image.LoadFromFile("Data/Map/Views/rivers.bmp"));
        HashSet<RiverConfig> riverDetails = GetRiverConfig();

        HashSet<RiverConfig> riverConfigs = [];
        foreach (RiverConfig river in riverDetails)
        {
            uint position = GetRiverPixel(river.StartPosition, ref riverData);
            if (position == startRiver || position == splitRiver)
            {
                riverConfigs.Add(river);
            }
            else
            {
                throw new ArgumentException($"River start position is invalid for {river.Name} at {position}" +
                                            $"/{MapDefines.GetIndexCoordinates((int)position)}");
            }
        }

        VegetationType riverType = GetRiverVegetationType();
        Dictionary<TypographyType, RiverBuilder> processedRivers = [];

        while (riverConfigs.Count != 0)
        {
            HashSet<RiverConfig> currentRivers = new HashSet<RiverConfig>(riverConfigs);
            foreach (RiverConfig river in currentRivers)
            {
                ProvinceId startProvince = MapDefines.GetProvinceId(river.StartPosition);
                RiverBuilder riverBuilder = new(river.Name, river.StartPosition, startProvince);
                int currentIndex = MapDefines.GetCoordinatesIndex(river.StartPosition);
                bool endpoint = false;
                
                if (riverData[currentIndex] == splitRiver)
                {
                    riverBuilder.riverStartpoint = true;
                    endpoint = true;
                    for (int direction = 0; direction < 4; direction++)
                    {
                        int directionIndex = GetIndexDirection(currentIndex, (RiverDirection)direction);
                        TerrainType terrain = _terrainMaps[directionIndex];
                        if (terrain.GetVegetation() == GetRiverVegetationType())
                        {
                            RiverDirection startingDirection = (RiverDirection)(((int)(RiverDirection)direction + 2) % 4);
                            riverBuilder.riverStartDirection = startingDirection;
                            riverBuilder.CurrentBehind = (RiverDirection)direction;
                            endpoint = false;
                            break;
                        }
                    }
                }
                
                //TODO Replace these two below
                TypographyType newType = new((ushort)(typographyCount + processedRivers.Count));
                Dictionary<int, (TypographyType, RiverDirection)> deltas = [];
                while (!endpoint)
                {
                    uint[] directions = GetRiverDirections(riverBuilder.CurrentBehind, currentIndex, riverData);
                    _terrainMaps[currentIndex] = new TerrainType(newType, riverType);
                    
                    if (directions.Contains(deltaRiver))
                    {
                        RiverDirection newDirection = (RiverDirection)Array.IndexOf(directions, deltaRiver);
                        deltas.Add(GetIndexDirection(currentIndex, newDirection), (newType, (RiverDirection)(((int)newDirection + 2) % 4)));
                    }
                    if (directions.Contains(endRiver) || (directions.Contains(mergeRiver) && !directions.Contains(baseRiver)))
                    {
                        uint endType = endRiver;
                        if (directions.Contains(mergeRiver) && !directions.Contains(baseRiver))
                        {
                            riverBuilder.riverEndpoint = true;
                            endType = mergeRiver;
                        }
                        if (deltas.Count != 0)
                        {
                            ProcessDeltas(riverBuilder, deltas, ref riverData);
                        }
                        RiverDirection endDirection = (RiverDirection)Array.IndexOf(directions, endType);
                        riverBuilder.Step(endDirection);
                        
                        currentIndex = GetIndexDirection(currentIndex, endDirection);
                        _terrainMaps[currentIndex] = new TerrainType(newType, riverType);
                        
                        Vector2I endPoint = MapDefines.GetIndexCoordinates(currentIndex);
                        riverBuilder.SetEndPoint(endPoint)
                            .SetEndProvince(MapDefines.GetProvinceId(endPoint));
                        processedRivers.Add(newType, riverBuilder);
                        riverConfigs.Remove(river);
                        
                        endpoint = true;
                    }
                    else if (directions.Contains(baseRiver))
                    {
                        currentIndex = ProcessStep(directions, riverBuilder, currentIndex, baseRiver);
                    }
                }
            }
        }
        

        foreach (var (type, riverBuilder) in processedRivers)
        {
            if (riverBuilder.riverEndpoint)
            {
                int endpoint = MapDefines.GetCoordinatesIndex(riverBuilder.endPoint);
                uint[] directions = GetRiverDirections(riverBuilder.CurrentBehind, endpoint, riverData);
                RiverDirection newDirection = (RiverDirection)Array.IndexOf(directions, baseRiver);
                int mergedRiver = GetIndexDirection(endpoint, newDirection);
                TerrainType endTerrain = _terrainMaps[mergedRiver];
                if (endTerrain.GetVegetation() == riverType)
                {
                    riverBuilder.SetEndRiver(endTerrain.GetTypography());
                }
                else
                {
                    throw new ArgumentException($"River end is invalid for {riverBuilder.name}");
                }
            }
            if (riverBuilder.riverStartpoint)
            {
                int startpoint = MapDefines.GetCoordinatesIndex(riverBuilder.startPoint);
                uint[] directions = GetRiverDirections(riverBuilder.riverStartDirection, startpoint, riverData);
                RiverDirection newDirection = (RiverDirection)Array.IndexOf(directions, baseRiver);
                int mergedRiver = GetIndexDirection(startpoint, newDirection);
                TerrainType startTerrain = _terrainMaps[mergedRiver];
                if (startTerrain.GetVegetation() == riverType)
                {
                    riverBuilder.SetStartRiver(startTerrain.GetTypography());
                }
                else
                {
                    throw new ArgumentException($"River start is invalid for {riverBuilder.name}");
                }
            }
            RiverDetails finalRiver = riverBuilder.AddType(type).Build();
            _rivers.Add(type, finalRiver);
        }

        return _rivers;
    }

    private int ProcessStep(uint[] directions, RiverBuilder riverBuilder, int currentIndex, uint typeRiver)
    {
        RiverDirection newDirection = (RiverDirection)Array.IndexOf(directions, typeRiver);
        riverBuilder.Step(newDirection);
        currentIndex = GetIndexDirection(currentIndex, newDirection);
        return currentIndex;
    }

    private void ProcessDeltas(
        RiverBuilder riverBuilder, 
        Dictionary<int, (TypographyType, RiverDirection)> newDeltas,
        ref ReadOnlySpan<uint> riverData)
    {
        foreach (var (startIndex, (type, direction)) in newDeltas)
        {
            Dictionary<int, (TypographyType, RiverDirection)> deltas = [];
            bool endpoint = false;
            int currentIndex = startIndex;
            RiverDirection currentBehind = direction;

            while (!endpoint)
            {
                uint[] directions = GetRiverDirections(currentBehind, currentIndex, riverData);
                _terrainMaps[currentIndex] = new TerrainType(type, GetRiverVegetationType());
                
                if (directions.Contains(deltaRiver))
                {
                    RiverDirection newDirection = (RiverDirection)Array.IndexOf(directions, deltaRiver);
                    deltas.Add(GetIndexDirection(currentIndex, newDirection), (type, (RiverDirection)(((int)newDirection + 2) % 4)));
                }
                if (directions.Contains(endRiver))
                {
                    if (deltas.Count != 0)
                    {
                        ProcessDeltas(riverBuilder, deltas, ref riverData);
                    }
                    RiverDirection endDirection = (RiverDirection)Array.IndexOf(directions, endRiver);
                    riverBuilder.Step(endDirection);
                    
                    currentIndex = GetIndexDirection(currentIndex, endDirection);
                    Vector2I endPoint = MapDefines.GetIndexCoordinates(currentIndex);
                    riverBuilder.AddDeltaEndpoint(endPoint);
                    
                    endpoint = true;
                }
                else if (directions.Contains(baseRiver))
                {
                    currentIndex = ProcessStep(directions, riverBuilder, currentIndex, baseRiver);
                    currentBehind = riverBuilder.CurrentBehind;
                }
                else
                {
                    throw new ArgumentException($"River end is invalid for {riverBuilder.name}");
                }
            }
        }
    }

    private uint[] GetRiverDirections(RiverDirection currentDirection, int currentIndex, ReadOnlySpan<uint> riverData)
    {
        uint[] directions = new uint[4];
        if (currentDirection != RiverDirection.North)
        {
            int northIndex = GetIndexDirection(currentIndex, RiverDirection.North);
            directions[(uint)RiverDirection.North] = riverData[northIndex];
        }
        if (currentDirection != RiverDirection.East)
        {
            int eastIndex = GetIndexDirection(currentIndex, RiverDirection.East);
            directions[(uint)RiverDirection.East] = riverData[eastIndex];
        }
        if (currentDirection != RiverDirection.South)
        {
            int southIndex = GetIndexDirection(currentIndex, RiverDirection.South);
            directions[(uint)RiverDirection.South] = riverData[southIndex];
        }
        if (currentDirection != RiverDirection.West)
        {
            int westIndex = GetIndexDirection(currentIndex, RiverDirection.West);
            directions[(uint)RiverDirection.West] = riverData[westIndex];
        }

        return directions;
    }

    private int GetIndexDirection(int currentIndex, RiverDirection direction)
    {
        switch (direction)
        {
            case RiverDirection.North:
                return currentIndex - MapDefines.MapWidth;
            case RiverDirection.East:
                return currentIndex + 1;
            case RiverDirection.South:
                return currentIndex + MapDefines.MapWidth;
            case RiverDirection.West:
                return currentIndex - 1;
            default:
                throw new ArgumentException($"Invalid direction {direction}");
        }
    }

    private VegetationType GetRiverVegetationType()
    {
        foreach ((_, VegetationDetails details) in MapDefines.TerrainVegetation)
        {
            if (details.Name == "River")
            {
                return details.Type;
            }
        }
        throw new InvalidOperationException("Could not find River VegetationType");
    }


    private HashSet<RiverConfig> GetRiverConfig()
    {
        HashSet<RiverConfig> riverData = [];
        using TextFieldParser csvParser = new TextFieldParser("Data/Map/Rivers/Rivers.csv");
        csvParser.SetDelimiters(",");
        csvParser.ReadLine();

        while (!csvParser.EndOfData)
        {
            // Read current line fields, pointer moves to the next line.
            string[] fields = csvParser.ReadFields();
            if (fields != null)
            {
                RiverConfig newRiver = new RiverConfig(Convert.ToInt32(fields[1]), Convert.ToInt32(fields[2]), fields[0]);
                riverData.Add(newRiver);
            }
            else
            {
                throw new NullReferenceException("Data/Map/Rivers/Rivers.csv is empty");
            }
        }

        return riverData;
    }
    
    private uint GetRiverPixel(Vector2I pixel, ref ReadOnlySpan<uint> data)
    {
        int index = MapDefines.GetCoordinatesIndex(pixel);
        return data[index];
    }
}

internal readonly record struct RiverConfig
{
    public Vector2I StartPosition { get; }
    public string Name { get; }

    public RiverConfig(int x, int y, string name)
    {
        StartPosition = new Vector2I(x, y);
        Name = name;
    }

    public override int GetHashCode()
    {
        return StartPosition.GetHashCode();
    }
}

                
                
            //     //MapDefines.TerrainMapData[currentIndex] = new TerrainType(newType, riverType);
            //     RiverDirection currentDirection = RiverDirection.None;
            //     if (newRiver.CurrentBehind != RiverDirection.North)
            //     {
            //         int northIndex = currentIndex - MapDefines.MapWidth;
            //         CheckDirection(northIndex, RiverDirection.North, riverData);
            //     }
            //     if (newRiver.CurrentBehind != RiverDirection.East)
            //     {
            //         int eastIndex = currentIndex + 1;
            //         CheckDirection(eastIndex, RiverDirection.East, riverData);
            //     }
            //     if (newRiver.CurrentBehind != RiverDirection.South)
            //     {
            //         int southIndex = currentIndex + MapDefines.MapWidth;
            //         CheckDirection(southIndex, RiverDirection.South, riverData);
            //     }
            //     if (newRiver.CurrentBehind != RiverDirection.West)
            //     {
            //         int westIndex = currentIndex - 1;
            //         CheckDirection(westIndex, RiverDirection.West, riverData);
            //         
            //     }
            //     if (currentDirection == RiverDirection.None)
            //     {
            //         throw new ArgumentException($"Could not find pixel in new direction. " +
            //                                     $"{river.Name} is drawn incorrectly");
            //     }
            //     newRiver.Step(currentDirection);
            //     currentIndex = newIndex;
            //     continue;
            //
            //     void CheckDirection(int directionIndex, RiverDirection newDirection, ReadOnlySpan<uint> riverData)
            //     {
            //         uint newColour = new Color(riverData[directionIndex]).ToUint();
            //         if (newColour == noRiver)
            //         {
            //             return;
            //         } 
            //         if (newColour == baseRiver)
            //         {
            //             currentDirection = newDirection;
            //             newIndex = directionIndex;
            //         }
            //         else if (newColour == deltaRiver)
            //         {
            //             deltas.Add(directionIndex, (newType, newDirection));
            //         }
            //         else if (newColour == mergeRiver || newColour == endRiver)
            //         {
            //             currentDirection = newDirection;
            //             newIndex = directionIndex;
            //             endpoint = true;
            //         }
            //     }
            // }
            //
            // //MapDefines.TerrainMapData[currentIndex] = new TerrainType(newType, riverType);
            // uint endColour = new Color(riverData[currentIndex]).ToUint();
            // if (endColour == mergeRiver)
            // {
            //     connectEnd.Add(newRiver);
            // }
            // else
            // {
            //     Vector2I currentPoint = MapDefines.GetIndexCoordinates(currentIndex);
            //     RiverDetails finishedRiver = newRiver.SetEndPoint(currentPoint)
            //         .SetEndProvince(MapDefines.GetProvinceId(currentPoint))
            //         .Build();