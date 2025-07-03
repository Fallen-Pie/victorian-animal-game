using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Provinces
{
    public interface IProvince
    {
        protected string GetDetails();
        protected void SetName();
        void GetNeighbours() { 
            throw new NotImplementedException();
        }
        void GetDistance() {
            throw new NotImplementedException();
        }
    }

    public partial class LandProvince : Node, IProvince
    {
        public Dictionary<CritterKey, Critter> critters = [];
            
        public void AddCritters(CritterKey newCritterKey, Critter newCritter) {
            critters.Add(newCritterKey, newCritter);
        }
        public string GetDetails()
        {
            return String.Format("Info on this LandProvince\nHashCode: {0}\nCurrent critter: {1}", GetHashCode(), critters);
        }

        public void SetName()
        {
            Name = String.Format("LandProvince-{0}", GetHashCode());
        }
    }

    public partial class SeaProvince : Node, IProvince
    {
        public string GetDetails()
        {
            throw new NotImplementedException();
        }

        public void SetName()
        {
            throw new NotImplementedException();
        }
    }

}
