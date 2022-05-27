using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class Bullet : MonoBehaviour
{
    public ParticleSystem boom;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float destroyAfterSeconds = 2;
    
    private PhysicsMovement _physicsMovement;

    public int Damage
    {
        get => damage;
    }

    private void Start()
    {
        
        _physicsMovement = GetComponent<PhysicsMovement>();
        Destroy(gameObject, destroyAfterSeconds);
    }

    private void FixedUpdate()
    {
        _physicsMovement.Move(transform.right);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        Destroy(Instantiate(boom.gameObject, transform.position, transform.rotation), destroyAfterSeconds);
    }
}