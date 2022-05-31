using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class Bullet : BulletBase
{
    public Color DamageColor;
    public ParticleSystem boom;

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

    public void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.layer == 10) // If player collision
            isDamageable = true;
    }

    public void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        var boomObj = Instantiate(boom.gameObject, transform.position, transform.rotation);
        if (isDamageable)
            boomObj.GetComponent<ParticleSystem>().startColor = DamageColor;

        Destroy(boomObj, destroyAfterSeconds);
    }
}