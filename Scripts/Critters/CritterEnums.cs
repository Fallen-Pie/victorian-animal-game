using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianAnimalGame.Scripts.Critters
{
    enum CritterSpecies : byte
    {
        Otter,
        Beaver,
        Raccon
    }

    enum CritterCulture : byte
    {
        SEALANDER,
        BURGUNIDAN,
        FRANCIAN
    }

    enum CritterClass : byte
    {
        UPPER,
        MIDDLE,
        LOWER,
        NONE
    }

    enum CritterOccupation : byte
    {
        Dependent,
        Labourer,
        Worker,
        Aristocrats,
        Buracrats
    }
}
