using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Province
{
    public partial class Province : Node
    {
        public Province() {
        }
        void GetNeighbours() { 
            throw new NotImplementedException();
        }
        void GetDistance() {
            throw new NotImplementedException();
        }
    }

    public partial class LandProvince : Province
    {
        private Dictionary<CritterKey, Critter> critters;

    }

    public partial class SeaProvince : Province
    {

    }

}
