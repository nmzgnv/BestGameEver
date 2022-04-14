using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _previousPosition;

    public Vector2 Velocity { get; private set; }

    private void FixedUpdate()
    {
        Velocity = (_rigidbody.position - _previousPosition) / Time.fixedDeltaTime;
        _previousPosition = _rigidbody.position;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector2 direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + direction * speed);
    }
}