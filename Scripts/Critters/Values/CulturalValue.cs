namespace VictorianAnimalGame.Scripts.Critters.Values;

public struct CulturalValue
{
    private float CurrentValue { get; set; }
    private float TrendStrength { get; set; }
    private float TargetValue { get; set; }
    private int CurrentStrength { get; set; }

    public void SetTarget(float newTarget)
    {
        TargetValue = newTarget;
    }
}
