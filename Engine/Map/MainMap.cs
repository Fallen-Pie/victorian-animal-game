using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Province;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.Critters.CritterBuilder;
using VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.ClassRatio;
using VictorianAnimalGame.Engine.Province.Critters.CritterBuilder.Distribution;
using VictorianAnimalGame.Engine.Province.Critters.Cultures;

namespace VictorianAnimalGame.Engine.Map;

public partial class MainMap : Node2D
{
    public override async void _Ready()
    {
        LandProvince province = new();
        province = InitialiseProvince(province);
        // foreach (var critter in province.ProvinceCritters) 
        // {
        //     GD.Print(critter);
        // }
        var frozenDictionary = GoodDefines.GoodTypes;

        //province.SetName();
        GD.Print(province.GetDetails());
        //AddChild(province);
        TestTime();
        base._Ready();
    }
        
    // static Random _r = new Random ();
    // static T RandomEnumValue<T> ()
    // {
    //     var v = Enum.GetValues(typeof (T)).Cast<T>().Where(value => value.ToString() != "None").ToArray();
    //     return (T) v.GetValue (_r.Next(v.Length));
    // }
        
    public LandProvince InitialiseProvince(LandProvince province)
    {
        CritterDefines.Species.TryGetValue(CritterDefines.SpeciesTypes["Otter"], out var otter);
        CritterDefines.Species.TryGetValue(CritterDefines.SpeciesTypes["Beaver"], out var beaver);

        HashSet<CritterEntry> critterEntries = new CritterBuilder()
            .SetAmount(30000)
            .SetDistribution(new Stage1Distribution())
            .SetRatio(new RuralRatio())
            .SetSpecies(otter)
            .SetCulture(new CultureType(0))
            .Build();
        province.AddCritterToProvince(critterEntries);
        
        critterEntries = new CritterBuilder()
            .SetAmount(10000)
            .SetDistribution(new FlatDistribution())
            .SetRatio(new RuralRatio())
            .SetSpecies(beaver)
            .SetCulture(new CultureType(0))
            .Build();
        province.AddCritterToProvince(critterEntries);
        return province;
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