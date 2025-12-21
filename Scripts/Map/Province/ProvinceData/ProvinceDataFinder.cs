using System.Collections.Generic;
using VictorianAnimalGame.Scripts.Critters;
using VictorianAnimalGame.Scripts.Map.Province.ProvinceData.Strategies;

namespace VictorianAnimalGame.Scripts.Map.Province.ProvinceData
{
    public class ProvinceDataFinder
    {
        private IDataStrategy _strategyMethod;

        public void ChangeBehaviour(IDataStrategy newStrategy)
        {
            _strategyMethod = newStrategy;
        }
        
        public HashSet<ProvinceCritterData> RunBehaviour(HashSet<CritterEntry> critters)
        {
            return _strategyMethod.Execute(critters);
        }
    }
}

