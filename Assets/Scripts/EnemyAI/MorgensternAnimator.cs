using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhysicsMovement))]
public class MorgensternAnimator : MonoBehaviour
{
    private readonly string runAnimationParameter = "Speed";
    private readonly string horizontalSpeedParameter = "HorizontalSpeed";
    private readonly string verticalSpeedParameter = "VerticalSpeed";
    private readonly string attackAnimationParameter = "Attack";
    private readonly string dieParameter = "Die";

    [SerializeField]
    private PlayerHealth playerHealth;

    private Animator _animator;
    private PhysicsMovement _physicsMovement;
    private float timer;

    private void Awake()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDie += PlayDieAnimation;
    }

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer < 0.1)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        var xSpeed = _physicsMovement.LastMoveDirection.x;
        var ySpeed = _physicsMovement.LastMoveDirection.y;
        if (Math.Abs(xSpeed) > Math.Abs(ySpeed))
        {
            _animator.SetFloat(horizontalSpeedParameter, xSpeed);
            _animator.SetFloat(verticalSpeedParameter, 0);
        }
        else
        {
            _animator.SetFloat(horizontalSpeedParameter, 0);
            _animator.SetFloat(verticalSpeedParameter, ySpeed);
        }

        _animator.SetFloat(runAnimationParameter, _physicsMovement.LastMoveDirection.magnitude);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(attackAnimationParameter);
    }

    private void PlayDieAnimation()
    {
        _animator.SetTrigger(dieParameter);
    }
}