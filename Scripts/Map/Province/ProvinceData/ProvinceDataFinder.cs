using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianAnimalGame.Scripts.Map.Province
{
    public class ProvinceDataFinder
    {
        private ProvinceDataContext _dataContext = new ();
    }

    internal class ProvinceDataContext
    {
        private IProvinceDataStrategy strategyMethod;
    }
}

