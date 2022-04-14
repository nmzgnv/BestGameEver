using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int curHealth;

    // Неуязвимость после удара
    [SerializeField]
    private float invulnerableTime;
    private float lastDamageTime;
    private bool IsInvulnerable => 
        Time.realtimeSinceStartup - lastDamageTime < invulnerableTime;

    // TODO: Добавить лайфбар и его обновление

    void Start()
    {
        curHealth = maxHealth;
        lastDamageTime = Time.realtimeSinceStartup;
    }

    
    void ReceiveDamage()
    {
        if (IsInvulnerable)
            return;
        
        curHealth--;
        lastDamageTime = Time.realtimeSinceStartup;

        if (curHealth == 0)
            Die();
    }

    void ReceiveHeal()
    {
        if (curHealth < maxHealth)
        {
            curHealth++;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
