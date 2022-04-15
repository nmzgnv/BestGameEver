using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private PhysicsMovement _physicsMovement;

    public int Damage
    {
        get => damage;
    }

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        Destroy(gameObject, 2.0F);
    }

    private void FixedUpdate()
    {
        _physicsMovement.Move(transform.right);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        Debug.Log("Bullet destroyed");
    }
}