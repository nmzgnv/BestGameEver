using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField]
    private PhysicsMovement movement;

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis(Axis.Horizontal);
        var vertical = Input.GetAxis(Axis.Vertical);

        movement.Move(new Vector2(horizontal, vertical));
    }
}