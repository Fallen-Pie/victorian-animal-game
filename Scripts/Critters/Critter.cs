using System;
using System.Text.RegularExpressions;

namespace VictorianAnimalGame.Scripts.Critters
{
    public class Critter
    {
        public uint critterCount;
        public uint educatedCount;
        public uint trainedCount;

        public Critter AddCritterCount(uint newCount)
        {
            critterCount += newCount;
            return this;
        }
        public Critter AddEducatedCount(uint newCount)
        {
            educatedCount += newCount;
            return this;
        }
        public Critter AddTrainedCount(uint newCount)
        {
            trainedCount += newCount;
            return this;
        }
        public Critter SubCritterCount(uint newCount)
        {
            critterCount -= newCount;
            return this;
        }
        public Critter SubEducatedCount(uint newCount)
        {
            educatedCount -= newCount;
            return this;
        }
        public Critter SubTrainedCount(uint newCount)
        {
            trainedCount -= newCount;
            return this;
        }
        public string GetDetails()
        {
            return String.Format("Info on this Critter:\nHashCode: {0}\nCurrent counts: {1}/{2}/{3}", GetHashCode(), critterCount, educatedCount, trainedCount);
        }
    }

    public class CritterBuilder 
    {
        private Critter _critter = new();

        public CritterBuilder SetCritterCount(uint newCount)
        {
            _critter.critterCount = newCount;
            return this;
        }
        public CritterBuilder SetEducatedCount(uint newEducated)
        {
            _critter.educatedCount = newEducated;
            return this;
        }
        public CritterBuilder SetCount(uint mewTrained)
        {
            _critter.trainedCount = mewTrained;
            return this;
        }
        public Critter Build()
        {
            return _critter;
        }
    }

}
