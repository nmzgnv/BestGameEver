using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private PhysicsMovement movement;

    [SerializeField] private PlayerAttack attack;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!_gameManager.IsPlayerControlled) return;
        if (Input.GetMouseButtonDown(0))
            attack.Attack();
    }

    private void FixedUpdate()
    {
        if (!_gameManager.IsPlayerControlled) return;
        var horizontal = Input.GetAxis(Axis.Horizontal);
        var vertical = Input.GetAxis(Axis.Vertical);

        movement.Move(new Vector2(horizontal, vertical));
    }
}