using UnityEngine;

public class HealthRestoreImprovement : ImprovementBase
{
    [SerializeField]
    private int restorePoints = 2;

    protected override void ApplyImprovement(Player player)
    {
        base.ApplyImprovement(player);
        player.Health.ReceiveHeal(restorePoints);
    }
}