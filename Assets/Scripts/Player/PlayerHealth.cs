using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnPlayerTakesDamage;
    public event Action OnPlayerApplyHeal;
    public event Action OnPlayerDie;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private float invulnerableTime;

    private int _currentHealth;
    private float _lastDamageTime;

    private bool IsInvulnerable =>
        Time.realtimeSinceStartup - _lastDamageTime < invulnerableTime;

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

    public void ReceiveDamage()
    {
        if (IsInvulnerable)
            return;

        _currentHealth--;
        _lastDamageTime = Time.realtimeSinceStartup;
        OnPlayerTakesDamage?.Invoke();

        if (_currentHealth <= 0)
            Die();
    }

    public void ReceiveHeal()
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth++;
            OnPlayerApplyHeal?.Invoke();
        }
    }

    private IEnumerator DestroyAfterDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void Die()
    {
        OnPlayerDie?.Invoke();
        
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        StartCoroutine(DestroyAfterDelay(1));
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
            ReceiveDamage();
    }
}