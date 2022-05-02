using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
    public Vector2 LastMoveDirection { get; private set; }
    public Vector2 LastViewDirection { get; private set; }
    
    public bool CanMove { get; set; } = true;

    [SerializeField]
    private float speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        if (!CanMove) return;
        _rigidbody.MovePosition(_rigidbody.position + direction.normalized * speed);
        LastMoveDirection = direction;
        if(direction.magnitude > 0.1)
            LastViewDirection = direction.normalized;
    }
}