using System;

namespace VictorianAnimalGame.Engine.Critters
{
    public record struct CritterDetails : IComparable<CritterDetails>
    {
        public short Year { get; }
        public ushort Dependants;
        public ushort Workers;
        public ushort Incapacitated;
        public ushort Soldiers;
        public int Total => Dependants + Workers + Incapacitated + Soldiers;
        
        public ushort Literate;
        public ushort Trained;
        public float LiteratePercentage => MathF.Round((float)Literate / Total * 100, 2);
        public float TrainedPercentage => MathF.Round((float)Trained / Total * 100, 2);

        public CritterDetails(short newYear, ushort dependants, ushort workers, 
            ushort incapacitated, ushort soldiers, ushort literate, ushort trained)
        {
            Year = newYear;
            Dependants = dependants;
            Workers = workers;
            Incapacitated = incapacitated;
            Soldiers = soldiers;
            Literate = literate;
            Trained = trained;
        }
        
        public int CompareTo(CritterDetails other)
        {
            return Year.CompareTo(other.Year);
        }

        public override string ToString()
        {
            return $"Year:{Year}|Total:{Total}/Dependants:{Dependants}/" +
                   $"Workers:{Workers}/Incapacitated:{Incapacitated}/Soldiers:{Soldiers}|" +
                   $"Literate:{Literate}, {LiteratePercentage}%/Trained:{Trained}, {TrainedPercentage}%";
        }
    }

    
}
