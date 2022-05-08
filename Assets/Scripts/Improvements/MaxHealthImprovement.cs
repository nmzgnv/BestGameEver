using UnityEngine;

public class MaxHealthImprovement : ImprovementBase
{
    [SerializeField]
    private int additionalHealth = 2;

    protected override void ApplyImprovement(Player player)
    {
        base.ApplyImprovement(player);
        player.Health.IncreaseMaxHealthAndHeal(additionalHealth);
    }
}