using System;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    private Transform _enemyTarget = null;

    public void SetEnemyTarget(Transform target)
    {
        _enemyTarget = target;
    }

    protected override void HandleSpawnedObject(GameObject spawnedObject)
    {
        if (_enemyTarget == null)
            throw new Exception("Spawner with enemies does not have target!");

        foreach (Transform child in spawnedObject.transform)
        {
            var weaponAI = child.GetComponent<WeaponAI>();
            if (weaponAI != null)
            {
                weaponAI.Target = _enemyTarget;
                break;
            }
        }
    }
}