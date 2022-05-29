using System;
using UnityEngine;

public class BossAIBase : MonoBehaviour
{
    public event Action AfterEnemiesSpawn;

    protected void InvokeAfterEnemiesSpawn()
    {
        AfterEnemiesSpawn?.Invoke();
    }
}