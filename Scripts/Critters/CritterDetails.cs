namespace VictorianAnimalGame.Scripts.Critters
{
    public class CritterDetails
    {
        private CritterOccupation _occupation;
        private uint _critterCount;
        private uint _critterTraining;
        private uint _critterHappiness;
        private uint _critterLiteracy;
        
        public uint GetCritterCount()
        {
            return _critterCount;
        }
        public void SetCritterAge(CritterOccupation newOccupation)
        {
            _occupation = newOccupation;
        }
        public void AddCritterCount(uint newCount)
        {
            _critterCount += newCount;
        }
        
        // public void SubCritterCount(uint newCount)
        // {
        //     _critterCount -= newCount;
        // }
        // public void AddCritterTraining(uint newCount)
        // {
        //     _critterTraining += newCount;
        // }
        // public void SubCritterTraining(uint newCount)
        // {
        //     _critterTraining -= newCount;
        // }
        // public void AddCritterHappiness(uint newCount)
        // {
        //     _critterHappiness += newCount;
        // }
        // public void SubCritterHappiness(uint newCount)
        // {
        //     _critterHappiness -= newCount;
        // }
        // public void AddCritterLiteracy(uint newCount)
        // {
        //     _critterLiteracy += newCount;
        // }
        // public void SubCritterLiteracy(uint newCount)
        // {
        //     _critterLiteracy -= newCount;
        // }
        public override string ToString()
        {
            return
                $"(Counts={_critterCount}/{_critterTraining} " +
                $"Rates={_critterHappiness}/{_critterLiteracy} " +
                $"AvailableJobs={_occupation})";
        }
    }
}
