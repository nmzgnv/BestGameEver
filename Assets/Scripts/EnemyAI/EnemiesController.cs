using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    public Transform AttackTarget { get; private set; }

    public void SetTarget(Transform target)
    {
        AttackTarget = target;
        foreach (var enemy in _enemies)
            SetUpEnemy(enemy, AttackTarget);
    }

    private void SetUpEnemy(Enemy enemy, Transform target)
    {
        enemy.EnemyAI.Target = target;
        enemy.WeaponAI.Target = target;
    }

    private void Awake()
    {
        _enemies = FindObjectsOfType<Enemy>().ToList();
    }
}