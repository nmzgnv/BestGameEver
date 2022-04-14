using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int curHealth;

    [SerializeField]
    private float invulnerableTime;
    private float lastDamageTime;
    private bool IsInvulnerable =>
        Time.realtimeSinceStartup - lastDamageTime < invulnerableTime;

    private void Start()
    {
        curHealth = maxHealth;
        lastDamageTime = Time.realtimeSinceStartup;
    }

    public void ReceiveDamage()
    {
        if (IsInvulnerable)
            return;

        curHealth--;
        lastDamageTime = Time.realtimeSinceStartup;

        if (curHealth == 0)
            Die();
    }

    public void ReceiveHeal()
    {
        if (curHealth < maxHealth)
            curHealth++;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}