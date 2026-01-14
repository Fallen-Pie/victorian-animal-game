using System;

namespace VictorianAnimalGame.Engine.Province.Critters
{
    public record struct CritterDetails
    {
        private CritterClassDetails _critterLower;
        private CritterClassDetails _critterMiddle;
        private CritterClassDetails _critterUpper;

        public CritterDetails(ushort low, ushort middle, ushort upper)
        {
            _critterLower = new CritterClassDetails(low);
            _critterMiddle = new CritterClassDetails(middle);
            _critterUpper = new CritterClassDetails(upper);
        }
        
        public uint GetCritterTotalCount()
        {
            return (uint)(_critterLower.Total + 
                   _critterMiddle.Total + 
                   _critterUpper.Total);
        }

        public uint GetCritterCount(CritterClass newCritterClass)
        {
            return newCritterClass switch
            {
                CritterClass.Lower => _critterLower.Total,
                CritterClass.Middle => _critterMiddle.Total,
                CritterClass.Upper => _critterUpper.Total,
                _ => throw new ArgumentException("Unknown CritterClass Value", nameof(newCritterClass))
            };
        }
        
        public void AddCritterCount(ushort newCount, CritterClass newCritterClass = CritterClass.Lower)
        {
            switch (newCritterClass)
            {
                case CritterClass.Lower:
                    _critterLower.Total += newCount;
                    break;
                case CritterClass.Middle:
                    _critterMiddle.Total += newCount;
                    break;
                case CritterClass.Upper:
                    _critterUpper.Total += newCount;
                    break;
                default:
                    throw new ArgumentException("Unknown CritterClass Value", nameof(newCritterClass));
            }
        }
        
        public override string ToString()
        {
            return $"LowerClass={_critterLower}|" +
                   $"MiddleClass={_critterMiddle}|" +
                   $"UpperClass={_critterUpper}";
        }

        private struct CritterClassDetails(ushort newCount)
        {
            public ushort Total = newCount;
            public ushort Trained;
            public ushort Literate;
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
