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
        var horizontal = 0;
        var vertical = 0;
        if (Input.GetKey(KeyCode.W)) vertical = 1;
        else if (Input.GetKey(KeyCode.S)) vertical = -1;
        if (Input.GetKey(KeyCode.D)) horizontal = 1;
        else if (Input.GetKey(KeyCode.A)) horizontal = -1;
        
        movement.Move(new Vector2(horizontal, vertical));
    }
}