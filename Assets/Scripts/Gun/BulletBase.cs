using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    public int Damage
    {
        get => damage;
    }
}