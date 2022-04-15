using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhysicsMovement))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private string runAnimationParameterName = "Optional";

    private Animator _animator;
    private PhysicsMovement _physicsMovement;

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(runAnimationParameterName, _physicsMovement.LastMoveDirection.magnitude);
    }
}