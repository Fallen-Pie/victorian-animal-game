using System.Collections.Generic;
using VictorianAnimalGame.Engine.Map.ProvinceData.Strategies;
using VictorianAnimalGame.Scripts.Critters;

namespace VictorianAnimalGame.Engine.Map.ProvinceData
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

