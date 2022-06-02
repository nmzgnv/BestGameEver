using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask damageableLayer;
    
    [SerializeField]
    private Transform attackRadiusCenter;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip hitSound;

    [SerializeField]
    private float movementFreezeTime;

    [SerializeField]
    private float secsDelayBetweenAttack;

    [SerializeField] private ParticleSystem blood;
    
    private float timer;

    public event Action OnPlayerAttacks;

    private PhysicsMovement _physicsMovement;
    private Camera _mainCamera;

    public void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.white;
        if (attackRadiusCenter == null) return;
        Gizmos.DrawWireSphere(attackRadiusCenter.position, attackRange);
#endif
    }

    private IEnumerator SetAttackState()
    {
        _physicsMovement.CanMove = false;
        yield return new WaitForSeconds(movementFreezeTime);
        _physicsMovement.CanMove = true;
    }

    public void Attack()
    {
        if (timer < secsDelayBetweenAttack)
            return;

        timer = 0;
        StartCoroutine(SetAttackState()); // Не может ходить во время анимации атаки

        OnPlayerAttacks?.Invoke();

        var viewVector = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - attackRadiusCenter.position;
        _physicsMovement.View(viewVector);

        var enemies = Physics2D.OverlapCircleAll(attackRadiusCenter.position, attackRange, damageableLayer);
        foreach (var enemy in enemies)
        {
            if (Vector2.Dot(enemy.transform.position - attackRadiusCenter.position, viewVector) < 0)
                continue;
            var enemyHealth = enemy.GetComponent<PlayerHealth>();
            var lootbox = enemy.GetComponent<LootBox>();
            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage();
                if (lootbox == null)
                {
                    var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    Instantiate(blood, enemyHealth.transform.position, randomRotation);
                }
            }
        }

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(hitSound);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }
}