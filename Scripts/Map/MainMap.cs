using Godot;
using System;
using System.Linq;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Critters.Species;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder;
using VictorianAnimalGame.Scripts.Defines;
using VictorianAnimalGame.Scripts.Map.Province;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.ClassRatio;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceBuilder.Distribution;

namespace VictorianAnimalGame.Scripts.Map {
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
            province.SetName();
            GD.Print(province.GetDetails());
            //AddChild(province);
            

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
            CritterDefines.Species.TryGetValue(CritterSpecies.Otter, out var value);
            newBuilder.SetSpecies(value);
            newBuilder.SetCulture(CritterCulture.Dutch);
            return newBuilder.AddCritterToProvince(30000, province);
        }
    }
}

