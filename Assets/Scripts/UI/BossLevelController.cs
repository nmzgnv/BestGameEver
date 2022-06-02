using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelController : MonoBehaviour
{
    [SerializeField] private PlayerHealth bossHealth;
    [SerializeField] private WinZoneController winZone;

    private void OnBossDied()
    {
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.Health.ReceiveDamage(enemy.Health.Health);
        }
        winZone.gameObject.SetActive(true);
    }
    
    private void Awake()
    {
        bossHealth.OnPlayerDie += OnBossDied;
    }
}
