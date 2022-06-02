using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MorgensternAI : BossAIBase
{
    [SerializeField]
    private MorgensternAnimator playerAnimator;

    [SerializeField]
    private PlayerHealth _playerHealth;

    [SerializeField]
    private BossRectangleWalking movementAI;

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

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip screamSound;

    private float _freezeParticlesSpeed = .1f;
    private const int NormalParticleSpeed = 1;


    private void PlayAttackEffects()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(screamSound);
        playerAnimator.PlayAttackAnimation();
    }

    private void AttackMusically()
    {
        PlayAttackEffects();
        musicalNotes.Play();
    }

    private void StopAttack()
    {
        musicalNotes.Stop();
    }


    private IEnumerator SimpleAttack()
    {
        AttackMusically();
        yield return new WaitForSeconds(simpleAttackSeconds);
        StopAttack();
    }

    private IEnumerator AttackWithFreeze()
    {
        AttackMusically();
        yield return new WaitForSeconds(simpleAttackSeconds);
        musicalNotes.playbackSpeed = _freezeParticlesSpeed;
        movementAI.CanMove = false;
        yield return new WaitForSeconds(freezeAttackSeconds);
        musicalNotes.playbackSpeed = NormalParticleSpeed;
        movementAI.CanMove = true;
        StartCoroutine(SimpleAttack());
    }

    private IEnumerator SpawnEnemiesAttack()
    {
        PlayAttackEffects();
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
        _playerHealth.OnPlayerDie += StopAllCoroutines;
        StartCoroutine(Loop());
    }
}