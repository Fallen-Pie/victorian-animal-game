using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianAnimalGame.Scripts.Critters
{
    public enum CritterSpecies : byte
    {
        Otter,
        Beaver,
        Raccon
    }

    public enum CritterCulture : byte
    {
        SEALANDER,
        BURGUNIDAN,
        FRANCIAN
    }

    public enum CritterClass : byte
    {
        UPPER,
        MIDDLE,
        LOWER,
        NONE
    }

    public enum CritterOccupation : byte
    {
        Dependent,
        Labourer,
        Worker,
        Aristocrats,
        Buracrats
    }
}
