using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianAnimalGame.Scripts.Critters
{
    public enum CritterSpecies : ushort
    {
        Otter,
        Beaver,
        Raccoon
    }

    public enum CritterClass : byte
    {
        UPPER,
        MIDDLE,
        LOWER,
        NONE
    }

    public enum CritterOccupation : ushort
    {
        Dependent,
        Labourer,
        Worker,
        Aristocrats,
        Bureaucrats
    }
    
    public enum CritterCulture : int
    {
        OtterAmericans,
        BeaverAmericans,
        RaccoonAmericans
    }
}
