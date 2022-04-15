using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimator animator;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask damageableLayer;

    [SerializeField]
    private Transform attackRadiusCenter;

    private Camera _camera;
    private Flipper _flipper;

    public void Start()
    {
        _flipper = GetComponent<Flipper>();
        _camera = Camera.main;
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
        animator.PlayAttackAnimation();

        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x && _flipper.isFlipped ||
            mousePosition.x < transform.position.x && !_flipper.isFlipped)
            _flipper.Flip();

        var enemies = Physics2D.OverlapCircleAll(attackRadiusCenter.position, attackRange, damageableLayer);
        foreach (var enemy in enemies)
        {
            var enemyHealth = enemy.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
                enemyHealth.ReceiveDamage();
        }
    }
}