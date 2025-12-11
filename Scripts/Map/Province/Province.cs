using System;
using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Map.Province;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData;

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
        public HashSet<CritterEntry> ProvinceCritters = [];
        public string ProvinceName;
        private readonly ProvinceDataFinder _provinceData = new();

        public void AddCritterEntry(CritterEntry newCritter) {
            if (!ProvinceCritters.Add(newCritter))
            {
                 ProvinceCritters.TryGetValue(newCritter, out var item);
                 item.GetCritterDetails().AddCritterCount(newCritter.GetCritterDetails().GetCritterCount());
            }
        }
        
        public string GetDetails()
        {
            string details = $"Info on this LandProvince\nHashCode: {GetHashCode()}\nCurrent Critters:\n";
            foreach (var critter in ProvinceCritters)
            {
                details += $"{critter}\n";
            }
            _provinceData.ChangeBehaviour(new ProvinceDataWorkforce());
            var i = _provinceData.RunBehaviour(ProvinceCritters);
            foreach (var critter in i)
            {
                details += $"{critter}\n";
            }
            
            _provinceData.ChangeBehaviour(new ProvinceDataSpeciesWorkforce());
            i = _provinceData.RunBehaviour(ProvinceCritters);
            foreach (var critter in i)
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

    public class SeaProvince : IProvince
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
