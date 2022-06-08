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

    [SerializeField]
    private float radius;

    [SerializeField]
    private int cooldownToNextTeleport;

    [SerializeField]
    private int teleportCost;

    [SerializeField]
    private float radiusToColliders;

    [SerializeField]
    private Transform teleportRadiusCenter;

    [SerializeField]
    private float teleportDownDelta = 0;

    public event Action OnTeleportDown;
    public event Action OnTeleportUp;
    public event Action OnTeleportFailed;

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
            targetPos.y -= teleportDownDelta;
            if (!IsFree(targetPos, .25f))
                targetPos.y += teleportDownDelta / 2;

            if (TryToTeleport(targetPos))
                _teleportationDelay = cooldownToNextTeleport;
            else
                OnTeleportFailed?.Invoke();
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
        return teleportRadiusCenter != null &&
               (teleportRadiusCenter.position - position).magnitude < radius;
    }

    private bool IsFree(Vector2 position, float radius = -1)
    {
        if (radius < 0)
            radius = radiusToColliders;
        return Physics2D.OverlapCircle(position, radius) == null;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (teleportRadiusCenter == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(teleportRadiusCenter.position, radius);
#endif
    }
}