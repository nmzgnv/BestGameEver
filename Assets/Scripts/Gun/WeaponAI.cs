using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float aimSpeed = 1;

    [SerializeField]
    private float restoreSpeed = 1;

    [SerializeField]
    private float ignoreRayCastRadius = 2;


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
        var targetPosition = target.position;
        var vectorToTarget = targetPosition - startPosition;

        var hitFrom = startPosition + ignoreRayCastRadius * vectorToTarget.normalized;
        var hit = Physics2D.Raycast(hitFrom, vectorToTarget);

        Debug.DrawLine(startPosition, hitFrom, Color.red); // ignored ray
        Debug.DrawLine(hitFrom, targetPosition, Color.blue);
        Debug.Log($"{hit.collider.gameObject.name}");

        var isPlayerVisible = hit.collider.GetComponent<PlayerAttack>() != null; // TODO change condition 
        _weapon.CanShoot = isPlayerVisible;

        if (!isPlayerVisible)
            vectorToTarget = _initialTransform.position - startPosition;

        var speed = isPlayerVisible ? aimSpeed : restoreSpeed;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}