using Godot;
using System;
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
                GD.Print(String.Format("{0}\n{1}", critter.Key.GetDetails(), critter.Value.GetDetails()));
            } 
            province.SetName();
            GD.Print(province.GetDetails());
            GD.Print(sizeof(int));
            this.AddChild(province);
            base._Ready();
        }
        public LandProvince AddCrittersToProvince(LandProvince province)
        {
            Random random = new();
            CritterKeyBuilder critterKeyBuilder;
            CritterBuilder critterBuilder;
            for (byte i = 0; i < 20; i++)
            {
                critterKeyBuilder = new();
                CritterKey critterKey = critterKeyBuilder.SetYear(i)
                    .SetSpecies(CritterSpecies.Otter)
                    .SetCulture(CritterCulture.SEALANDER)
                    .SetOccupation(CritterOccupation.Labourer)
                    .Build();
                critterBuilder = new();
                Critter critter = critterBuilder.SetCritterCount((uint)random.Next(1, 12000)).Build();
                province.AddCritters(critterKey, critter);
            }
            return province;
        }
    }
}

