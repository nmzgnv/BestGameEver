using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D _rigidbody;

    public Vector2 LastMoveDirection { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector2 direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + direction * speed);
        LastMoveDirection = direction;
    }
}