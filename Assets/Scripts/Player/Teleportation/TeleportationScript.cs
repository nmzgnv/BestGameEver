using System;
using System.Linq;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    /*
    TODO: Нужно прикрутить анимации и обрабатывать со всей физикой
    */
    private Camera _mainCamera;
    private int _teleportationDelay = 0;
    private ManaController _manaController;

    [SerializeField] private float radius;
    [SerializeField] private int cooldownToNextTeleport;
    [SerializeField] private int teleportCost;
    [SerializeField] private float radiusToColliders;

    [SerializeField] private Animator _animator;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _manaController = GetComponent<ManaController>();
    }

    void FixedUpdate()
    {
        _teleportationDelay = Math.Max(_teleportationDelay - 1, 0);
        
        if (_teleportationDelay == 0 && Input.GetMouseButton(1))
        {
            var targetPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;

            if (TryToTeleport(targetPos))
            {
                _teleportationDelay = cooldownToNextTeleport;
            }
        }
    }

    private bool TryToTeleport(Vector3 targetPos)
    {
        return InRadius(targetPos) 
               && IsFree(targetPos)
               && _manaController.TryDoAction(teleportCost, () => transform.position = targetPos);
    }

    private bool InRadius(Vector3 position)
    {
        return (transform.position - position).magnitude < radius;
    }

    private bool IsFree(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, radiusToColliders) == null;
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        #endif
    }
}
