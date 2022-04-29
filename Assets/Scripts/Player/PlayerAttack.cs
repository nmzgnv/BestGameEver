using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask damageableLayer;

    [SerializeField]
    private Transform attackRadiusCenter;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip hitSound;

    private Camera _camera;
    private Flipper _flipper;
    private PlayerAnimator _animator;

    public void Start()
    {
        _flipper = GetComponent<Flipper>();
        _camera = Camera.main;
        _animator = GetComponent<PlayerAnimator>();
    }

    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackRadiusCenter.position, attackRange);
#endif
    }

    public void Attack()
    {
        _animator.PlayAttackAnimation();

        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x && _flipper.isFlipped ||
            mousePosition.x < transform.position.x && !_flipper.isFlipped)
            _flipper.Flip();
        
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(hitSound);
        
        var enemies = Physics2D.OverlapCircleAll(attackRadiusCenter.position, attackRange, damageableLayer);
        foreach (var enemy in enemies)
        {
            var enemyHealth = enemy.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
                enemyHealth.ReceiveDamage();
        }
    }
}