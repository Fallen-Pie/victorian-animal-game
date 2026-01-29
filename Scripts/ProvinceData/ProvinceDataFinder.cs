using System.Collections.Generic;
using VictorianAnimalGame.Engine.Critters;
using VictorianAnimalGame.Scripts.ProvinceData.Strategies;

namespace VictorianAnimalGame.Scripts.ProvinceData
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

