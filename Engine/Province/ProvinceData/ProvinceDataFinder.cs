using System.Collections.Generic;
using VictorianAnimalGame.Engine.Province.Critters;
using VictorianAnimalGame.Engine.Province.ProvinceData.Strategies;

namespace VictorianAnimalGame.Engine.Province.ProvinceData
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

