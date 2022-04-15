using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;
    public void Attack()
    {
        animator.PlayAttackAnimation();
    }


}
