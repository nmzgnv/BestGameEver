using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MorgensternAI : BossAIBase
{
    [SerializeField]
    private PlayerAnimator playerAnimator;

    [SerializeField]
    private MorgensternMovementAI movementAI;

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

    [SerializeField]
    private int movementSeconds = 10;

    [SerializeField]
    private int secondsDelayAfterAttack = 2;

    private void MusicAttack()
    {
        playerAnimator.PlayAttackAnimation();
        musicalNotes.Play();
    }

    private void StopAttack()
    {
        musicalNotes.Stop();
    }


    private IEnumerator SimpleAttack()
    {
        MusicAttack();
        yield return new WaitForSeconds(simpleAttackSeconds);
        StopAttack();
    }

    private IEnumerator AttackWithFreeze()
    {
        MusicAttack();
        yield return new WaitForSeconds(simpleAttackSeconds);
        musicalNotes.Pause();
        movementAI.CanMove = false;
        yield return new WaitForSeconds(freezeAttackSeconds);
        movementAI.CanMove = true;
        StartCoroutine(SimpleAttack());
    }

    private IEnumerator SpawnEnemiesAttack()
    {
        playerAnimator.PlayAttackAnimation();
        foreach (var point in spawnEnemyPoints)
        {
            var randomIndex = Random.Range(0, availableEnemies.Length);
            var randomEnemy = availableEnemies[randomIndex];
            Instantiate(randomEnemy, point.position, transform.rotation);
        }

        InvokeAfterEnemiesSpawn();
        yield break;
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            movementAI.CanMove = true;
            yield return new WaitForSeconds(movementSeconds);
            movementAI.CanMove = false;
            var possibleAttacks = new Func<IEnumerator>[] {SimpleAttack, AttackWithFreeze, SpawnEnemiesAttack};
            var randomAttack = possibleAttacks[Random.Range(0, possibleAttacks.Length)];
            StartCoroutine(randomAttack());
            yield return new WaitForSeconds(secondsDelayAfterAttack);
        }
    }

    private void Start()
    {
        StartCoroutine(Loop());
    }
}