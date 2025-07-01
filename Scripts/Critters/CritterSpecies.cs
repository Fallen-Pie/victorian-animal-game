using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianAnimalGame.Scripts.Critters
{
    interface ICritterSpecies
    {
        string toString();
        string getDetails();
    }

    internal class CritterSpecies : ICritterSpecies
    {
        protected string name = String.Empty;
        protected float workrate = 1.0f;
        protected float birthrate = 1.0f;
        protected byte averageAge = 16;
        protected byte adulthoodAge = 8;
        protected byte educationAge = 4;
        protected byte maxAge = 24;
        
        public string getDetails()
        {
            throw new NotImplementedException();
        }

        public string toString()
        {
            return String.Format("{0}", name);
        }
    }

    internal class Otter : CritterSpecies
    {
        public Otter() {
            name = "Otter";
            workrate = 1.1f;
        }
    }

    internal class Raccon : CritterSpecies
    {
        public Raccon()
        {
            name = "Raccon";
            workrate = 1.1f;
        }
    }

    internal class Beaver : CritterSpecies
    {
        public Beaver()
        {
            name = "Beaver";
            workrate = 1.2f;
        }
    }
}
