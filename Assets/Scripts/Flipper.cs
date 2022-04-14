using System;
using UnityEngine;


[RequireComponent(typeof(PhysicsMovement))]
public class Flipper : MonoBehaviour
{
    private PhysicsMovement _physicsMovement;

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
    }


    private void Update()
    {
        var horizontalDirection = _physicsMovement.Velocity.x;
        Debug.Log($"{horizontalDirection} {Math.Sign(transform.localScale.x)} {Math.Sign(horizontalDirection)}");
        if (horizontalDirection != 0 && Math.Sign(transform.localScale.x) != Math.Sign(horizontalDirection))
            Flip();
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}