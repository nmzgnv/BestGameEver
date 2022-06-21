using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool toDestroyAfterDeath = true;
    public const int LimitHealth = 12;
    public event Action OnPlayerTakesDamage;
    public event Action OnPlayerApplyHeal;
    public event Action OnPlayerDie;

    [HideInInspector]
    public bool IsDead;

    [SerializeField]
    [Range(1, LimitHealth)]
    private int maxPossibleHealth = 6;

    [SerializeField]
    [Range(1, LimitHealth)]
    private int maxHealth = 3;

    [SerializeField]
    private float invulnerableTime;

    [SerializeField]
    private float destroyAfterSecs = 1;

    private int _currentHealth;
    private float _lastDamageTime;

    private bool IsInvulnerable =>
        Time.realtimeSinceStartup - _lastDamageTime < invulnerableTime;

    public int MaxPossibleHealth => maxPossibleHealth;
    public int MaxHealth => maxHealth;
    public int Health => _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    private void Start()
    {
        _lastDamageTime = Time.realtimeSinceStartup;
    }

    public void ReceiveDamage(int damage = 1)
    {
        if (IsInvulnerable)
            return;

        _currentHealth -= damage;
        _lastDamageTime = Time.realtimeSinceStartup;
        OnPlayerTakesDamage?.Invoke();

        if (_currentHealth <= 0)
            Die();
    }

    public void ReceiveHeal(int points = 1)
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth = Mathf.Min(_currentHealth + points, maxHealth);
            OnPlayerApplyHeal?.Invoke();
        }
    }

    public void IncreaseMaxHealthAndHeal(int points = 1)
    {
        maxHealth = Math.Min(maxHealth + points, LimitHealth);
        _currentHealth = maxHealth;
        OnPlayerApplyHeal?.Invoke();
    }

    private IEnumerator DestroyAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void Die()
    {
        IsDead = true;
        OnPlayerDie?.Invoke();

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        if(toDestroyAfterDeath)
            StartCoroutine(DestroyAfterDelay(destroyAfterSecs));
    }

    private void ReceiveDamageIfBullet(GameObject other)
    {
        var bullet = other.GetComponent<BulletBase>();
        if (bullet)
            ReceiveDamage(bullet.Damage);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        ReceiveDamageIfBullet(other.gameObject);
    }

    public void OnParticleCollision(GameObject other)
    {
        ReceiveDamageIfBullet(other);
    }
}