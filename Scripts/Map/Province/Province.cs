using System;
using VictorianAnimalGame.Scripts.Map.Province;

namespace VictorianAnimalGame.Scripts.Map.Province
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
}
