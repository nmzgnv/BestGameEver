using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelController : MonoBehaviour
{
    [SerializeField] private PlayerHealth bossHealth;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private string nextSceneName;
    
    private void OnBossDied()
    {
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.Health.ReceiveDamage(enemy.Health.Health);
        }

        if (sceneChanger != null)
            StartCoroutine(ShowCutScene());
    }

    private IEnumerator ShowCutScene()
    {
        yield return new WaitForSeconds(2);
        sceneChanger.ChangeScene(nextSceneName);
        sceneChanger.gameObject.GetComponent<Animator>().speed = 0.1f;
    }
    
    private void Awake()
    {
        bossHealth.OnPlayerDie += OnBossDied;
    }
}
