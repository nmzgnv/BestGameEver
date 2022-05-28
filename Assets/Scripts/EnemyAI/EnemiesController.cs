using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    private BossAIBase _boss;
    private Transform _target;
    public Transform AttackTarget { get; private set; }

    public void SetTarget(Transform target)
    {
        if (_target == null)
            _target = target;
        AttackTarget = target;
        foreach (var enemy in _enemies)
            SetUpEnemy(enemy, AttackTarget);
    }

    private void SetUpEnemy(Enemy enemy, Transform target)
    {
        enemy.EnemyAI.Target = target;
        enemy.WeaponAI.Target = target;
    }

    private void FindEnemies()
    {
        _enemies = FindObjectsOfType<Enemy>().ToList();
    }

    private void SetupAllEnemies()
    {
        FindEnemies();
        SetTarget(_target);
    }

    private void Awake()
    {
        FindEnemies();

        _boss = FindObjectOfType<BossAIBase>();
        _boss.AfterEnemiesSpawn += SetupAllEnemies;
    }
}