using System;

namespace VictorianAnimalGame.Scripts.Critters
{
    public record struct CritterDetails
    {
        private CritterClass _critterLower;
        private CritterClass _critterMiddle;
        private CritterClass _critterUpper;

        public CritterDetails(uint low, uint middle, uint upper)
        {
            _critterLower = new CritterClass(low);
            _critterMiddle = new CritterClass(middle);
            _critterUpper = new CritterClass(upper);
        }
        
        public uint GetCritterCount()
        {
            return _critterLower.Total + 
                   _critterMiddle.Total + 
                   _critterUpper.Total;
        }
        public void AddCritterCount(uint newCount)
        {
            _critterLower.Total += newCount;
        }
        
        public override string ToString()
        {
            return $"LowerClass={_critterLower}|" +
                   $"MiddleClass={_critterMiddle}|" +
                   $"UpperClass={_critterUpper}";
        }

        private struct CritterClass(uint newCount)
        {
            public uint Total = newCount;
            public uint Trained;
            public uint Literate;
            //public uint Love;
            //public uint Hate;

            public override string ToString()
            {
                return
                    $"({Total}" +
                    $"/{Trained}/{Literate})";
                //$"Rates={Love}/{Hate})";
            }
        }
    }

    
}
