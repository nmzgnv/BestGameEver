using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private EnemyAI enemyAI;

    [SerializeField]
    private WeaponAI weaponAI;

    public PlayerHealth Health => playerHealth;
    public EnemyAI EnemyAI => enemyAI;
    public WeaponAI WeaponAI => weaponAI;
}