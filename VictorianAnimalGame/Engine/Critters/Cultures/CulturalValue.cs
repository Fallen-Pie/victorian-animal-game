namespace VictorianAnimalGame.Engine.Critters.Cultures;

public struct CulturalValue
{
    private bool Active { get; set; }
    private float CurrentValue { get; set; }
    private float TrendStrength { get; set; }
    private float TargetValue { get; set; }
    private float CurrentStrength { get; set; }

    // public void SetTarget(float newTarget)
    // {
    //     TargetValue = newTarget;
    // }
}
