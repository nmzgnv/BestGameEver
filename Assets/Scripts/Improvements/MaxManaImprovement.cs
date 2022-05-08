using UnityEngine;

public class MaxManaImprovement : ImprovementBase
{
    [SerializeField]
    private int additionalManaPoints = 100;

    protected override void ApplyImprovement(Player player)
    {
        base.ApplyImprovement(player);
        player.Mana.IncreaseMaxManaAndRestore(additionalManaPoints);
    }
}