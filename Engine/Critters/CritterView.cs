using System;
using VictorianAnimalGame.Engine.Defines;

namespace VictorianAnimalGame.Engine.Critters;

public readonly ref struct CritterView
{
    private readonly CritterDetails _details;
    private readonly int _index;

    public CritterView(CritterDetails newDetails)
    {
        _details = newDetails;
    }

    public int Year => DateDefines.Year - _details.Length + _index;
    public int Dependants => _details.Dependants[_index];
    public int Workers => _details.Dependants[_index];
    public int Incapacitated => _details.Dependants[_index];
    public int Soldiers => _details.Dependants[_index];
    //public int Occupied => Workers + Soldiers;
    public int Total => Dependants + Workers + Incapacitated + Soldiers;
        
    public int Literate => _details.Literate[_index];
    public int Trained => _details.Trained[_index];
    public float LiteratePercentage => MathF.Round((float)Literate / Total * 100, 2);
    public float TrainedPercentage => MathF.Round((float)Trained / Total * 100, 2);
    
    public override string ToString()
    {
         return $"Year:{Year}|Total:{Total}/Dependants:{Dependants}/" +
                $"Workers:{Workers}/Incapacitated:{Incapacitated}/Soldiers:{Soldiers}|" +
                $"Literate:{Literate}, {LiteratePercentage}%/Trained:{Trained}, {TrainedPercentage}%";
    }
}