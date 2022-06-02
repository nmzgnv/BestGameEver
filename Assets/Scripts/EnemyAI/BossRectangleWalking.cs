using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using Random = System.Random;

public class BossRectangleWalking : MonoBehaviour
{
    private PhysicsMovement _physicsMovement;
    private Random _random = new Random();

    [SerializeField]
    private Transform center;

    [SerializeField]
    private List<Transform> targets;

    [SerializeField]
    private Seeker _seeker;

    [SerializeField]
    private float minimalMoveVectorLength = .2f;

    private List<Vector3> _currentPath = new List<Vector3>();
    private Transform _curTarget;
    private int _curIndex;
    private int _pathInd = 0;
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        //targets = GameObject.FindGameObjectsWithTag("BossTarget").Select(x => x.transform).ToList();
        _curTarget = targets[0];
        _curIndex = 0;
    }

    private void OnPathCalculated(Path path)
    {
        _currentPath = path.vectorPath;
        _pathInd = 0;
    }

    private void CalculatePath()
    {
        if ((_curTarget.position - center.position).magnitude < 0.5)
        {
            // var index = _random.Next(targets.Count);
            // if (_curTarget == targets[index])
            //     index = (index + 1) % targets.Count;
            //
            // _curTarget = targets[index];
            _curIndex = (_curIndex + 1) % targets.Count;
            _curTarget = targets[_curIndex];
        }

        _seeker.StartPath(center.position, _curTarget.position, OnPathCalculated);
    }

    private void Start()
    {
        CalculatePath();
    }

    private void Update()
    {
        if (!CanMove)
            Debug.Log(CanMove);
        if (!CanMove || center == null || _curTarget == null)
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
