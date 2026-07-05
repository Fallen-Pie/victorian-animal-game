using FixedMathSharp;
using VictorianAnimalGame.Engine.Components.Production.Labour;

namespace VictorianAnimalGame.Engine.Components.Production.Processes;

public class Process(string newName, LabourAmount neededLabour, ProcessModifier modifer, int fullValue)
{
    public readonly string Name = newName;
    public readonly ProcessModifier Modifier = modifer;
    //TODO Add tags for a Process
    //public readonly List<ProcessType> Types { get; init; }

    //TODO Add Goods Requirement
    //private readonly GoodsAmount RequiredGoods { get; init; }
    //TODO Add Ratio for how important Goods/vs needed Labour is for the process
    //private int LabourGoodRatio { get; init; }

    public Fixed64 AdjustProcess(Fixed64 scale, LabourAmount currentLabour)
    {
        Fixed64 value = (Fixed64)currentLabour.Amount / (scale * (int)neededLabour.Amount);
        return fullValue * value;
    }
}

