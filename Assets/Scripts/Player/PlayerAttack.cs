using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;
    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private Flipper _flipper;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
        _camera = Camera.main;
    }

    public void OnDrawGizmosSelected()
    {
        var mousePosition = Input.mousePosition;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition), 2);
    }

    public void Attack()
    {
        animator.PlayAttackAnimation();

        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x && _flipper.isFlipped ||
            mousePosition.x < transform.position.x && !_flipper.isFlipped)
            _flipper.Flip();
    }

    
}
