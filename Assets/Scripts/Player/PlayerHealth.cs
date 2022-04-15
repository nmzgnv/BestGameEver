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
        Debug.Log($"Remain {curHealth} HP");

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

    public void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
            ReceiveDamage();
    }
}