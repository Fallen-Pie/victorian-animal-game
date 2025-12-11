using Godot;
using System;
using System.Runtime.InteropServices;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Map.Provinces;

namespace VictorianAnimalGame.Scripts.Map {
    public partial class MainMap : Node2D
    {
        public override void _Ready()
        {
            LandProvince province = new();
            province = AddCrittersToProvince(province);
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
            var v = Enum.GetValues (typeof (T));
            return (T) v.GetValue (_r.Next(v.Length));
        }
        
        public LandProvince AddCrittersToProvince(LandProvince province)
        {
            for (int i = 0; i < 10024; i++)
            {
                CritterDetails critterDetails = new();
                critterDetails.AddCritterCount((uint)_r.Next(1, 12000));
                CritterEntry critterEntry = new(RandomEnumValue<CritterSpecies>(), 
                    RandomEnumValue<CritterOccupation>(), RandomEnumValue<CritterCulture>(), 
                    critterDetails);
                province.AddCritterEntry(critterEntry);
                Console.WriteLine($"Size of {typeof(CritterDetails)} is {Marshal.SizeOf(typeof(CritterEntry))}");
            }
            return province;
        }
    }
}

