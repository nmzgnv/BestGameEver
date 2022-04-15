using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimator animator;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask damageableLayer;

    private Camera _camera;
    private Flipper _flipper;

    public void Start()
    {
        _flipper = GetComponent<Flipper>();
        _camera = Camera.main;
    }

    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        var attackPoint = transform.position + new Vector3(!_flipper.isFlipped ? .5f : -.5f, 0, 0);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint, attackRange);
#endif
    }

    public void Attack()
    {
        animator.PlayAttackAnimation();

        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x && _flipper.isFlipped ||
            mousePosition.x < transform.position.x && !_flipper.isFlipped)
            _flipper.Flip();

        var attackPoint = transform.position + new Vector3(!_flipper.isFlipped ? .5f : -.5f, 0, 0);
        var enemies = Physics2D.OverlapCircleAll(attackPoint, attackRange, damageableLayer);
        foreach (var enemy in enemies)
            enemy.GetComponent<PlayerHealth>().ReceiveDamage();
    }
}