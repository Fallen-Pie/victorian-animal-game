using System;

namespace VictorianAnimalGame.Engine.Province
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
