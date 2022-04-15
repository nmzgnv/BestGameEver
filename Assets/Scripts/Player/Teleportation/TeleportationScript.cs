using System;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    /*
    TODO: Нужно прикрутить анимации и обрабатывать со всей физикой
    */
    // [SerializeField]
    // private PhysicsMovement movement;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var targetPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(targetPos.x, targetPos.y, 0);
        }
    }
}
