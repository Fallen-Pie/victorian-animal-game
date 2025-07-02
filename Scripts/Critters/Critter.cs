using System;
using System.Text.RegularExpressions;

namespace VictorianAnimalGame.Scripts.Critters
{
    internal class Critter
    {
        protected uint critterCount;
        protected uint educatedCount;
        protected uint trainedCount;

        public Critter SetCount(uint newCount, uint newEducated = 0, uint mewTrained = 0)
        {
            critterCount = newCount;
            educatedCount = newEducated;
            trainedCount = mewTrained;
            return this;
        }
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
            return String.Format(@"Info on this Critter:
                HashCode: {0}
                Current counts: {1}/{2}/{3}", GetHashCode(), critterCount, educatedCount, trainedCount);
        }
    }
}
