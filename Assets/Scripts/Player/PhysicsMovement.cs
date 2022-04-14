using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private string runAnimationParameterName = "Optional";

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + direction * speed);
        var horizontalDirection = direction.x;

        if (horizontalDirection != 0 && Math.Sign(transform.localScale.x) != Math.Sign(horizontalDirection))
            Flip();

        if (_animator != null)
            _animator.SetFloat(runAnimationParameterName, direction.magnitude);
    }


    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}