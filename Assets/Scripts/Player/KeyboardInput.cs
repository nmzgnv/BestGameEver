using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField]
    private PhysicsMovement movement;

    [SerializeField]
    private PlayerAttack attack;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            attack.Attack();
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis(Axis.Horizontal);
        var vertical = Input.GetAxis(Axis.Vertical);

        movement.Move(new Vector2(horizontal, vertical));
    }
}