using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float reachedPositionDistance;

    [SerializeField]
    private float sightRange;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private Transform target;

    private PhysicsMovement physicsMovement;

    private Seeker seeker;
    private List<Vector3> currentPath;
    private int currentWaypoint;
    private bool reachedEndOfPath;

    private Vector3 startingPosition;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        physicsMovement = GetComponent<PhysicsMovement>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        currentPath = new List<Vector3>() { startingPosition };
        currentWaypoint = 0;
        reachedEndOfPath = true;
    }

    private void Update()
    {
        UpdatePath();
        MoveByPath();
    }

    private void UpdatePath()
    {
        if (IsPlayerInSightArea())
        {
            Vector3 vectorToTarget = target.position - transform.position;
            vectorToTarget *= (vectorToTarget.magnitude - attackRange) / vectorToTarget.magnitude;
            seeker.StartPath(transform.position, transform.position + vectorToTarget, OnPathComplete);
        }
        else
        {
            float length = currentPath.GetRange(currentWaypoint, currentPath.Count - currentWaypoint).Sum(vector => vector.magnitude);
            if (length < 3 * reachedPositionDistance)
                seeker.StartPath(transform.position, GetRoamingPosition(), OnPathComplete);
        }
    }

    private void MoveByPath()
    {
        if (currentPath == null || reachedEndOfPath)
            return;

        if (Vector3.Distance(transform.position, currentPath[currentWaypoint]) < reachedPositionDistance)
        {
            currentWaypoint++;   
        }
        if (currentWaypoint >= currentPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        physicsMovement.Move(currentPath[currentWaypoint] - transform.position);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0f, 5f);
    }

    private bool IsPlayerInSightArea()
    {
        var vectorToTarget = target.position - transform.position;
        var hitFrom = transform.position + 0.3f * vectorToTarget.normalized;
        var hit = Physics2D.Raycast(hitFrom, vectorToTarget);
        Debug.DrawLine(hitFrom, hitFrom + vectorToTarget * (sightRange / vectorToTarget.magnitude), Color.green);
        var isPlayerVisible = hit.collider.GetComponent<Player>() != null;
        return vectorToTarget.magnitude < sightRange && isPlayerVisible;
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            currentPath = path.vectorPath;
            currentWaypoint = 0;
            reachedEndOfPath = false;
        }
    }
}
