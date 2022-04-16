using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _attackRadiusCenter;

    [SerializeField]
    private Transform _bulletTarget;

    public Transform BulletTarget
    {
        get => _bulletTarget;
    }

    public Transform AttackRadiusCenter
    {
        get => _attackRadiusCenter;
    }
}