using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    [SerializeField] private LevelBarContoller levelBar;
    public Transform AttackTarget { get; private set; }

    public int EnemiesCount => _enemies.Count;

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
        foreach (var enemy in _enemies)
            enemy.Health.OnPlayerDie += levelBar.RefreshBar;
    }
}