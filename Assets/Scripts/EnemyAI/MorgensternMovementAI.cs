using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class MorgensternMovementAI : MonoBehaviour
{
    private PhysicsMovement _physicsMovement;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Seeker _seeker;


    private void Awake()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
    }

   
}