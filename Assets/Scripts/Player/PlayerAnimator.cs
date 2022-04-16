using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhysicsMovement))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private string runAnimationParameter = "Speed";
    [SerializeField] private string attackAnimationParameter = "Attack";
    [SerializeField] private string takingDamageParameter = "TakeDamage";
    [SerializeField] private string dieParameter = "Die";

    private Animator _animator;
    private PhysicsMovement _physicsMovement;

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(runAnimationParameter, _physicsMovement.LastMoveDirection.magnitude);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(attackAnimationParameter);
    }

    public void PlayTakeDamageAnimation()
    {
        _animator.SetTrigger(takingDamageParameter);
    }

    public void PlayDieAnimation()
    {
        _animator.SetTrigger(dieParameter);
    }
}