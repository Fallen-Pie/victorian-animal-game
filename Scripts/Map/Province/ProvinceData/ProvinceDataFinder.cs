using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData
{
    public class ProvinceDataFinder
    {
        private IProvinceDataStrategy _strategyMethod;

        public void ChangeBehaviour(IProvinceDataStrategy newStrategy)
        {
            _strategyMethod = newStrategy;
        }
        
        public HashSet<ProvinceCritterData> RunBehaviour(HashSet<CritterEntry> critters)
        {
            return _strategyMethod.Execute(critters);
        }
    }
}

