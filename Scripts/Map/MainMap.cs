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
            foreach (var critter in province.critters) 
            {
                GD.Print(critter);
            } 
            province.SetName();
            GD.Print(province.GetDetails());
            AddChild(province);
            base._Ready();
        }
        public LandProvince AddCrittersToProvince(LandProvince province)
        {
            Random random = new();
            CritterType critterType = new(CritterSpecies.Otter, CritterOccupation.Labourer, new CritterCulture(Culture.OtterAmericans));
            for (byte i = 0; i < 16; i++)
            {
                Critter newCritter = new();
                newCritter.AddCritterCount((uint)random.Next(1, 12000));
                newCritter.AddCulture(critterType);
                province.AddCritters(newCritter);
                Console.WriteLine($"Size of {typeof(Critter)} is {Marshal.SizeOf(typeof(CritterType))}");
            }
            return province;
        }
    }
}

