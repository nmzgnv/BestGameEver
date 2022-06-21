using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class Bullet : BulletBase
{
    public Color DamageColor;
    public ParticleSystem boom;
    public ParticleSystem blood;

    [SerializeField]
    private float destroyAfterSeconds = 2;

    private PhysicsMovement _physicsMovement;
    private bool isDamageable = false;

    private void Start()
    {
        _physicsMovement = GetComponent<PhysicsMovement>();
        Destroy(gameObject, destroyAfterSeconds);
    }

    private void FixedUpdate()
    {
        _physicsMovement.Move(transform.right);
    }

    private void HandleBulletPenetration(Collider2D other)
    {
        if (other.gameObject.layer == 7) // If is ignoreBullet object
            return;
        Destroy(gameObject);
        if (other.gameObject.layer == 10) // If player collision
            isDamageable = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        HandleBulletPenetration(other);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        HandleBulletPenetration(other.collider);
    }

    public void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        if (!isDamageable) Instantiate(boom.gameObject, transform.position, transform.rotation);
        if (isDamageable)
        {
            Instantiate(blood.gameObject, transform.position, transform.rotation);
        }
    }
}