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
    
    [SerializeField]
    private UIBar healthBar;
    private PlayerAnimator _animator;

    private void Start()
    {
        curHealth = maxHealth;
        lastDamageTime = Time.realtimeSinceStartup;
        _animator = GetComponent<PlayerAnimator>();
        healthBar.SetMaxValue(maxHealth);
    }

    public void ReceiveDamage()
    {
        if (IsInvulnerable)
            return;

        curHealth--;
        lastDamageTime = Time.realtimeSinceStartup;
        _animator.PlayTakeDamageAnimation();
        healthBar.SetValue(curHealth);
        
        if (curHealth == 0)
            Die();
    }

    public void ReceiveHeal()
    {
        if (curHealth < maxHealth)
            curHealth++;
        healthBar.SetValue(curHealth);
    }
    
    private void Die()
    {
        _animator.PlayDieAnimation();
        GetComponent<Rigidbody2D>().simulated = false;
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
            ReceiveDamage();
    }
}