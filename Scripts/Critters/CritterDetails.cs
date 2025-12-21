namespace VictorianAnimalGame.Scripts.Critters
{
    public class CritterDetails
    {
        private CritterLifeStage _lifeStage = CritterLifeStage.Young;
        
        private uint _critterCount;
        private uint _critterTrained;
        private uint _critterLiterate;
        private uint _critterHappiness;

        public uint GetCritterCount()
        {
            return _critterCount;
        }
        public void SetCritterAge(CritterLifeStage newLifeStage)
        {
            _lifeStage = newLifeStage;
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
                $"(Counts={_critterCount}/{_critterTrained} " +
                $"Rates={_critterHappiness}/{_critterLiterate} " +
                $"AvailableJobs={_lifeStage})";
        }
    }
}
