using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhysicsMovement))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly string runAnimationParameter = "Speed";
    private readonly string horizontalSpeedParameter = "HorizontalSpeed";
    private readonly string verticalSpeedParameter = "VerticalSpeed";
    private readonly string horizontalViewParameter = "HorizontalView";
    private readonly string verticalViewParameter = "VerticalView";
    private readonly string attackAnimationParameter = "Attack";
    private readonly string takingDamageParameter = "TakeDamage";
    private readonly string dieParameter = "Die";
    private readonly string teleportUpParameter = "TeleportUp";
    private readonly string teleportDownParameter = "TeleportDown";

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private TeleportationScript playerTeleport;
    
    private Animator _animator;
    private PhysicsMovement _physicsMovement;

    private void Awake()
    {
        if (playerHealth != null)
        {
            playerHealth.OnPlayerTakesDamage += PlayTakeDamageAnimation;
            playerHealth.OnPlayerDie += PlayDieAnimation;
        }

        if (playerAttack != null)
        {
            playerAttack.OnPlayerAttacks += PlayAttackAnimation;
        }

        if (playerTeleport != null)
        {
            playerTeleport.OnTeleportDown += PlayTeleportDownAnimation;
            playerTeleport.OnTeleportUp += PlayTeleportUpAnimation;
        }
    }
    
    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(horizontalSpeedParameter, _physicsMovement.LastMoveDirection.x);
        _animator.SetFloat(verticalSpeedParameter, _physicsMovement.LastMoveDirection.y);
        _animator.SetFloat(horizontalViewParameter, _physicsMovement.LastViewDirection.x);
        _animator.SetFloat(verticalViewParameter, _physicsMovement.LastViewDirection.y);
        _animator.SetFloat(runAnimationParameter, _physicsMovement.LastMoveDirection.magnitude);
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger(attackAnimationParameter);
    }

    private void PlayTakeDamageAnimation()
    {
        _animator.SetTrigger(takingDamageParameter);
    }

    private void PlayDieAnimation()
    {
        _animator.SetTrigger(dieParameter);
    }
    
    public void PlayTeleportDownAnimation()
    {
        _animator.SetTrigger(teleportDownParameter);
    }

    public void PlayTeleportUpAnimation()
    {
        _animator.SetTrigger(teleportUpParameter);
    }
}