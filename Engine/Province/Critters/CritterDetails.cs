namespace VictorianAnimalGame.Engine.Province.Critters
{
    public record struct CritterDetails
    {
        private CritterClass _critterLower;
        private CritterClass _critterMiddle;
        private CritterClass _critterUpper;

        public CritterDetails(ushort low, ushort middle, ushort upper)
        {
            _critterLower = new CritterClass(low);
            _critterMiddle = new CritterClass(middle);
            _critterUpper = new CritterClass(upper);
        }
        
        public uint GetCritterCount()
        {
            return (uint)(_critterLower.Total + 
                   _critterMiddle.Total + 
                   _critterUpper.Total);
        }
        public void AddCritterCount(ushort newCount)
        {
            _critterLower.Total += newCount;
        }
        
        public override string ToString()
        {
            return $"LowerClass={_critterLower}|" +
                   $"MiddleClass={_critterMiddle}|" +
                   $"UpperClass={_critterUpper}";
        }

        private struct CritterClass(ushort newCount)
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
