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
    private Camera _mainCamera;
    
    public void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _mainCamera = FindObjectOfType<Camera>();
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

        
        var viewVector = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - attackRadiusCenter.position;
        _physicsMovement.View(viewVector);

        var enemies = Physics2D.OverlapCircleAll(attackRadiusCenter.position, attackRange, damageableLayer);
        foreach (var enemy in enemies)
        {
            if (Vector2.Dot(enemy.transform.position - attackRadiusCenter.position, viewVector) < 0)
                continue;
            var enemyHealth = enemy.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
                enemyHealth.ReceiveDamage();
        }
    }
}