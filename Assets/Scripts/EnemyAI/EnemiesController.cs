using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    private BossAIBase _boss;
    private Transform _target;

    [SerializeField]
    private LevelBarContoller levelBar;

    public Transform AttackTarget { get; private set; }

    public int EnemiesCount => _enemies.Count;

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
        foreach (var enemy in _enemies)
            if(levelBar != null)
                enemy.Health.OnPlayerDie += levelBar.RefreshBar;
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
        if (_boss != null)
            _boss.AfterEnemiesSpawn += SetupAllEnemies;
    }
}