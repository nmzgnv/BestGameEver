using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MorgensternAI : BossAIBase
{
    [SerializeField]
    private ParticleSystem musicalNotes;

    [SerializeField]
    private float simpleAttackSeconds;

    [SerializeField]
    private float freezeAttackSeconds;

    [SerializeField]
    private List<Transform> spawnEnemyPoints;

    [SerializeField]
    private GameObject[] availableEnemies = new GameObject[0];

    // 1. Атакует нотами какой-то промежуток времени
    // 2. Атакует нотами какой-то промежуток времени => нотызавис ают на какйо-то промежуток => отвисают на какой-то промежуток
    // 3. Атакует спавном врагов


    private void Attack()
    {
        musicalNotes.Play();
    }

    private void StopAttack()
    {
        musicalNotes.Stop();
    }


    private IEnumerator SimpleAttack()
    {
        Attack();
        yield return new WaitForSeconds(simpleAttackSeconds);
        StopAttack();
    }

    private IEnumerator AttackWithFreeze()
    {
        Attack();
        yield return new WaitForSeconds(simpleAttackSeconds);
        musicalNotes.Pause();
        yield return new WaitForSeconds(freezeAttackSeconds);
        StartCoroutine(SimpleAttack());
    }

    private void SpawnEnemiesAttack()
    {
        foreach (var point in spawnEnemyPoints)
        {
            var randomIndex = Random.Range(0, availableEnemies.Length);
            var randomEnemy = availableEnemies[randomIndex];
            Instantiate(randomEnemy, point.position, transform.rotation);
        }

        InvokeAfterEnemiesSpawn();
    }

    private void Start()
    {
    }
}