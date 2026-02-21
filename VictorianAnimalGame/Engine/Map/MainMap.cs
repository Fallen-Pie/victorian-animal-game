using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Engine.Critters.CritterBuilder;
using VictorianAnimalGame.Engine.Critters.CritterBuilder.ClassRatio;
using VictorianAnimalGame.Engine.Critters.CritterBuilder.Distribution;
using VictorianAnimalGame.Engine.Critters.Cultures;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;

namespace VictorianAnimalGame.Engine.Map;

public partial class MainMap : Node2D
{
    public override async void _Ready()
    {
        List<LandProvince> provinces = [];
        Random rnd = new Random();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            LandProvince province = new();
            province = InitialiseProvince(province, rnd);
            province.SetData();
            provinces.Add(province);
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        // foreach (var critter in province.ProvinceCritters) 
        // {
        //     GD.Print(critter);
        // }
        var frozenDictionary = GoodDefines.GoodTypes;

        //province.SetName();
        int randomProvince = rnd.Next(0, 1000);
        GD.Print(provinces[randomProvince].GetDetails());
        GD.Print(provinces[randomProvince].GetData());
        //AddChild(province);
        Console.WriteLine($"Time for Provinces: {elapsedMs}ms");
        for (int i = 0; i < 1000; i++)
        {
            ProcessYear(provinces, randomProvince);
            Console.WriteLine($"Year: {i}");
        }
        TestTime();
        base._Ready();
    }
        
    // static Random _r = new Random ();
    // static T RandomEnumValue<T> ()
    // {
    //     var v = Enum.GetValues(typeof (T)).Cast<T>().Where(value => value.ToString() != "None").ToArray();
    //     return (T) v.GetValue (_r.Next(v.Length));
    // }
        
    public LandProvince InitialiseProvince(LandProvince province, Random rnd)
    {
        CritterDefines.SpeciesTypes.TryGetValue("Otter", out var otter);
        CritterDefines.SpeciesTypes.TryGetValue("Beaver", out var beaver);

        HashSet<CritterEntry> critterEntries = new CritterBuilder()
            .SetAmount((uint)rnd.Next(20000, 40000))
            .SetDistribution(new Stage1Distribution())
            .SetRatio(new RuralRatio())
            .SetSpecies(otter)
            .SetCulture(new CultureType(0))
            .Build();
        province.AddCritterToProvince(critterEntries);
        
        critterEntries = new CritterBuilder()
            .SetAmount((uint)rnd.Next(5000, 15000))
            .SetDistribution(new FlatDistribution())
            .SetRatio(new RuralRatio())
            .SetSpecies(beaver)
            .SetCulture(new CultureType(0))
            .Build();
        province.AddCritterToProvince(critterEntries);
        return province;
    }

    public void ProcessYear(List<LandProvince> provinces, int randomProvince)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (int week = 1; week <= DateDefines.WeeksPerYear; week++)
        {
            foreach (var province in provinces)
            {
                province.ProcessWeekly();
            }
            for (int day = 1; day <= DateDefines.WeeklyDaysAmount; day++)
            {
                foreach (var province in provinces)
                {
                    province.ProcessDaily();
                }
            }
            Console.WriteLine(provinces[randomProvince].GetData());
        }
        watch.Stop();
        Console.WriteLine($"Time for Province Processing: {watch.ElapsedMilliseconds}ms");
    }

    public async void TestTime()
    {
        while (true)
        {
            await Task.Delay(100);
            DateDefines.IncrementTime();
            Console.WriteLine(DateDefines.GetTime());
        }

        //return await Task.FromResult(0);
    }
}