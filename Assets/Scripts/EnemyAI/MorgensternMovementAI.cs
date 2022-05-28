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

    private List<Vector3> _currentPath = new List<Vector3>();
    private int _pathInd = 0;
    private bool _isPathGenerated = false;


    private void Awake()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
    }

    private IEnumerator GoByPath(List<Vector3> path)
    {
        Debug.Log($"Path: {path.Count}");
        foreach (var direction in path)
        {
            _physicsMovement.Move(direction);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnPathCalculated(Path path)
    {
        _pathInd = 0;
        _isPathGenerated = false;
    }

    private void Update()
    {
        // генерим путь до персонажа.
        // Идем по пути. Когда путь пройден => повторяем
        _seeker.StartPath(transform.position, target.position, OnPathCalculated).vectorPath;
    }
}