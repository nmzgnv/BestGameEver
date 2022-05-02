using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    /*
    TODO: Нужно прикрутить анимации и обрабатывать со всей физикой
    */
    private Camera _mainCamera;
    private int _teleportationDelay = 0;
    private ManaController _manaController;
    private PhysicsMovement _physicsMovement;

    [SerializeField] private float radius;
    [SerializeField] private int cooldownToNextTeleport;
    [SerializeField] private int teleportCost;
    [SerializeField] private float radiusToColliders;
    
    public event Action OnTeleportDown;
    public event Action OnTeleportUp;

    // Не бейте, понятия не имею как без этих костылей делать... а так работает, а работает - не трогай
    // При телепорте запоминаем позицию куда хотели попасть, запускаем анимацию ухода
    // По окончание анимации, запускается Event с TeleportUp (внутри самой анимации)
    private Vector3 _targetPosition;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _manaController = GetComponent<ManaController>();
        _physicsMovement = GetComponent<PhysicsMovement>();
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

    
    public void TeleportUp()
    {
        transform.position = _targetPosition;
        _physicsMovement.CanMove = true;
        OnTeleportUp?.Invoke();
    }

    private bool TryToTeleport(Vector3 targetPos)
    {
        return InRadius(targetPos) 
               && IsFree(targetPos)
               && _manaController.TryDoAction(teleportCost, () =>
               {
                   _targetPosition = targetPos;
                   _physicsMovement.CanMove = false;
                   OnTeleportDown?.Invoke();
               });
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
