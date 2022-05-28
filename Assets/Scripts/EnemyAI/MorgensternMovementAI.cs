using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class MorgensternMovementAI : MonoBehaviour
{
    private PhysicsMovement _physicsMovement;

    [SerializeField]
    private Transform center;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Seeker _seeker;

    [SerializeField]
    private float minimalMoveVectorLength = .2f;

    private List<Vector3> _currentPath = new List<Vector3>();
    private int _pathInd = 0;
    public bool CanMove { get; set; } = true;


    private void Awake()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
    }

    private void OnPathCalculated(Path path)
    {
        _currentPath = path.vectorPath;
        _pathInd = 0;
    }

    private void CalculatePath()
    {
        _seeker.StartPath(center.position, target.position, OnPathCalculated);
    }

    private void Start()
    {
        CalculatePath();
    }

    private void Update()
    {
        if (!CanMove || center == null || target == null)
        {
            _physicsMovement.LastMoveDirection = Vector2.zero;
            return;
        }

        if (_currentPath.Count > _pathInd)
        {
            var deltaVector = _currentPath[_pathInd] - center.position;
            if (deltaVector.magnitude > minimalMoveVectorLength)
                _physicsMovement.Move(deltaVector);
            _pathInd++;
        }
        else
            CalculatePath();
    }
}