using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    [SerializeField]
    private float aimSpeed = 1;

    [SerializeField]
    private float restoreSpeed = 1;

    [SerializeField]
    private float ignoreRayCastRadius = 2;

    [SerializeField]
    private float minShootRadius = 2;

    private Weapon _weapon;
    private Transform _initialTransform;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();

        _initialTransform = transform;
    }

    private void Update()
    {
        var startPosition = transform.position;

        if (Target == null)
            return;

        var targetPosition = Target.position;
        var vectorToTarget = targetPosition - startPosition;

        var hitFrom = startPosition + ignoreRayCastRadius * vectorToTarget.normalized;
        var hit = Physics2D.Raycast(hitFrom, vectorToTarget);

        Debug.DrawLine(startPosition, hitFrom, Color.red); // ignored ray
        Debug.DrawLine(hitFrom, targetPosition, Color.blue);
        
        var isPlayerVisible = hit.collider.GetComponent<PlayerAttack>() != null; // TODO change condition 

        if (!isPlayerVisible)
            vectorToTarget = _initialTransform.position - startPosition;

        _weapon.CanShoot = isPlayerVisible && vectorToTarget.magnitude > minShootRadius;

        var speed = isPlayerVisible ? aimSpeed : restoreSpeed;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}