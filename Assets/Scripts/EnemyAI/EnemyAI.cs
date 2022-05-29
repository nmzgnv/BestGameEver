using System.Collections.Generic;
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
    private float lastMoveTime;

    private Vector3 startingPosition;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        physicsMovement = GetComponent<PhysicsMovement>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        currentPath = new List<Vector3>() {startingPosition};
        currentWaypoint = 0;
        reachedEndOfPath = true;
        lastMoveTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (target == null)
            return;

        UpdatePath();
        MoveByPath();
    }

    private void UpdatePath()
    {
        if (Time.realtimeSinceStartup - lastMoveTime > 1f)
        {
            seeker.StartPath(transform.position, GetRoamingPosition(), OnPathComplete);
        }
        else if (IsPlayerInSightArea())
        {
            Vector3 vectorToTarget = target.position - transform.position;
            vectorToTarget *= (vectorToTarget.magnitude - attackRange) / vectorToTarget.magnitude;
            seeker.StartPath(transform.position, transform.position + vectorToTarget, OnPathComplete);
        }
        else if (reachedEndOfPath)
        {
            seeker.StartPath(transform.position, GetRoamingPosition(), OnPathComplete);
        }
    }

    private void MoveByPath()
    {
        if (currentPath == null || reachedEndOfPath)
            return;

        while (currentWaypoint < currentPath.Count
               && Vector3.Distance(transform.position, currentPath[currentWaypoint]) < reachedPositionDistance)
        {
            currentWaypoint++;
            lastMoveTime = Time.realtimeSinceStartup;
        }

        if (currentWaypoint >= currentPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        Debug.DrawLine(transform.position, currentPath[currentWaypoint], Color.magenta);

        physicsMovement.Move(currentPath[currentWaypoint] - transform.position);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition +
               new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0f, 5f);
    }

    private bool IsPlayerInSightArea()
    {
        var vectorToTarget = target.position - transform.position;
        var hitFrom = transform.position;
        var hit = Physics2D.Raycast(hitFrom, vectorToTarget);
        Debug.DrawLine(hitFrom, hitFrom + vectorToTarget * (sightRange / vectorToTarget.magnitude), Color.yellow);
        var isPlayerVisible = (hit.collider != null ? hit.collider.GetComponent<Player>() : null) != null;
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