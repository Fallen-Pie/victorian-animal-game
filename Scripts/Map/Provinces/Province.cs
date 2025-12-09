using System;
using System.Collections.Generic;
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

    public class LandProvince : IProvince
    {
        public HashSet<CritterEntry> _provinceCritters = [];
        public string ProvinceName;

        public void AddCritterEntry(CritterEntry newCritter) {
            if (!_provinceCritters.Add(newCritter))
            {
                 _provinceCritters.TryGetValue(newCritter, out var item);
                 item.GetCritterDetails().AddCritterCount(newCritter.GetCritterDetails().GetCritterCount());
            }
        }
        
        public string GetDetails()
        {
            string details = $"Info on this LandProvince\nHashCode: {GetHashCode()}\nCurrent Critters:\n";
            foreach (var critter in _provinceCritters)
            {
                details += $"{critter}\n";
            }
            return details;
        }

        public void SetName()
        {
            ProvinceName = $"LandProvince-{GetHashCode()}";
        }
    }

    public partial class SeaProvince : IProvince
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
