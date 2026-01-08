using System;
using System.Linq;
using Godot;
using VictorianAnimalGame.Engine.Defines;
using VictorianAnimalGame.Engine.Map.Province;
using VictorianAnimalGame.Engine.Map.Province.ProvinceBuilder;
using VictorianAnimalGame.Engine.Map.Province.ProvinceBuilder.ClassRatio;
using VictorianAnimalGame.Engine.Map.Province.ProvinceBuilder.Distribution;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Engine.Map {
    public partial class MainMap : Node2D
    {
        public override void _Ready()
        {
            LandProvince province = new();
            province = InitialiseProvince(province);
            foreach (var critter in province.ProvinceCritters) 
            {
                GD.Print(critter);
            }
            var frozenDictionary = GoodDefines.GoodTypes;

            province.SetName();
            GD.Print(province.GetDetails());
            //AddChild(province);
            TestTime(3000);
            base._Ready();
        }
        
        static Random _r = new Random ();
        static T RandomEnumValue<T> ()
        {
            var v = Enum.GetValues(typeof (T)).Cast<T>().Where(value => value.ToString() != "None").ToArray();
            return (T) v.GetValue (_r.Next(v.Length));
        }
        
        public LandProvince InitialiseProvince(LandProvince province)
        {
            ProvinceCritterBuilder newBuilder = new ProvinceCritterBuilder();
            newBuilder.SetDistribution(new Stage1Distribution());
            newBuilder.SetRatio(new RuralRatio());
            CritterDefines.Species.TryGetValue(CritterDefines.SpeciesTypes["Otter"], out var value);
            newBuilder.SetSpecies(value);
            newBuilder.SetCulture(CritterCulture.Dutch);
            return newBuilder.AddCritterToProvince(30000, province);
        }

        public void TestTime(int count)
        {
            for (int i = 0; i < count; i++)
            {
                DateDefines.IncrementTime();
                Console.WriteLine(DateDefines.GetTime());
            }
        }
    }
}

