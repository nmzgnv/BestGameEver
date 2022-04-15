using System;
using UnityEngine;


[RequireComponent(typeof(PhysicsMovement))]
public class Flipper : MonoBehaviour
{
    [HideInInspector]
    public bool isFlipped;
    
    private PhysicsMovement _physicsMovement;
    private Transform _transform;

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _transform = transform;
        if (transform.localScale.x > 0)
            isFlipped = false;
    }


    private void Update()
    {
        var horizontalDirection = _physicsMovement.LastMoveDirection.x;
        if (horizontalDirection != 0 && Math.Sign(_transform.localScale.x) != Math.Sign(horizontalDirection))
            Flip();
    }

    public void Flip()
    {
        var scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
        isFlipped = !isFlipped;
    }
}