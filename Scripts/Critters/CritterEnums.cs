using System;

namespace VictorianAnimalGame.Scripts.Critters
{
    public enum CritterSpecies : ushort
    {
        None = 0,
        Otter,
        Beaver,
        Raccoon
    }

    public enum CritterCulture : ushort
    {
        None = 0,
        Dutch,
        Americans,
        British,
        Irish
    }
    
    [Flags]
    public enum CritterOccupation : ushort
    {
        Dependent = 0,
        Manager = 0b_0000_0001,
        Student = 0b_0000_0010,
        Conscript = 0b_0000_0100,
        Labourer = 0b_0000_1000,
        Farmer = 0b_0001_0000,
        Artisan = 0b_0010_0000,
        Worker = 0b_0100_0000,
        Ruler = 0b_1000_0000,
        Administrator = 0b_0001_0000_0000,
        Regular = 0b_0010_0000_0000,
        Officer = 0b_0100_0000_0000,
        Scholar = 0b_1000_0000_0000,
    
        Labour = Labourer | Farmer | Artisan | Worker,
        Clerk = Ruler | Administrator | Scholar | Manager,
        Solider = Conscript | Regular | Officer,
    
        //Young = Dependent,
        //Adolescent = Student | Worker | Dependent,
        //Adult = Clerk | Labour | Solider | Dependent,
    
        LowerClass = Labourer | Farmer | Worker | Conscript | Student,
        MiddleClass = Administrator | Scholar | Regular | Artisan,
        UpperClass = Officer | Ruler | Manager,
    }

    public enum CritterLifeStage : ushort
    {
        Young,
        Adolescent,
        Adult,
        Elder
    }
}


