namespace VictorianAnimalGame.Scripts.Critters
{
    public interface ICritter
    {
        void AddCritterCount(uint newCount);
    }
    
    public class Critter : ICritter
    {
        private uint _critterCount;
        private uint _critterTraining;
        private float _critterHappiness;
        private float _critterLiteracy;
        private CritterType CritterType { get; set; }

        public void AddCritterCount(uint newCount)
        {
            _critterCount += newCount;
        }
        public void SubCritterCount(uint newCount)
        {
            _critterCount -= newCount;
        }
        public void AddCritterTraining(uint newCount)
        {
            _critterTraining += newCount;
        }
        public void SubCritterTraining(uint newCount)
        {
            _critterTraining -= newCount;
        }
        public void AddCritterHappiness(float newCount)
        {
            _critterHappiness += newCount;
        }
        public void SubCritterHappiness(float newCount)
        {
            _critterHappiness -= newCount;
        }
        public void AddCritterLiteracy(float newCount)
        {
            _critterLiteracy += newCount;
        }
        public void SubCritterLiteracy(float newCount)
        {
            _critterLiteracy -= newCount;
        }
        public void AddCulture(CritterType newCritterType)
        {
            CritterType = newCritterType;
        }

        public override string ToString()
        {
            return
                $"Critter: HashCode={GetHashCode()} Counts={_critterCount}/{_critterTraining} Rates={_critterHappiness}/{_critterLiteracy}";
        }
    }
}
