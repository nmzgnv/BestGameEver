using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRestoreImprovement : ImprovementBase
{
    [SerializeField]
    private int manaRestorePoints = 300;

    protected override void ApplyImprovement(Player player)
    {
        base.ApplyImprovement(player);
        player.Mana.AddMana(manaRestorePoints);
    }
}