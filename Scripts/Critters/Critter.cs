using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianAnimalGame.Scripts.Critters
{
    internal class Critter(CritterCulture culture, CritterSpecies species)
    {
        public int Count { get; set; }
        readonly CritterCulture Culture = culture;
        readonly CritterSpecies Species = species;

        public void AddCritters(int amount) { Count += amount; }

        public void RemoveCritters(int amount) { Count -= amount; }

    }
}
