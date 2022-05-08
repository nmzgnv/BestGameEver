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
        Debug.Log($"Enemy {enemy.name} set up");
    }

    private void Start()
    {
        _enemies = FindObjectsOfType<Enemy>().ToList();
        Debug.Log($"enemies count: {_enemies.Count}");
    }
}