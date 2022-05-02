using System;
using System.Collections;
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
    
    public event Action OnPlayerAttacks;
    
    private PhysicsMovement _physicsMovement;

    public void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
    }

    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.white;
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
        StartCoroutine(SetAttackState());

        OnPlayerAttacks?.Invoke();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(hitSound);

        var enemies = Physics2D.OverlapCircleAll(attackRadiusCenter.position, attackRange, damageableLayer);
        foreach (var enemy in enemies)
        {
            var enemyHealth = enemy.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
                enemyHealth.ReceiveDamage();
        }
    }
}