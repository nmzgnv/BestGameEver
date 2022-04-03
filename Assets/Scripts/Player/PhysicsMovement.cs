using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;


    [SerializeField]
    private PlayerAnimationController animationController;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + direction * speed);
        var horizontalDirection = direction.x;

        if (horizontalDirection != 0 && Math.Sign(transform.localScale.x) != Math.Sign(horizontalDirection))
            Flip();

        animationController.SetSpeed(direction.magnitude);
    }


    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}