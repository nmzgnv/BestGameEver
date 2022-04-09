using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    /*
    TODO: Скорее всего нужно искать mainCamera на Awake или Start
    TODO: Нужно прикрутить анимации и обрабатывать со всей физикой
    */
    // [SerializeField]
    // private PhysicsMovement movement;
    [SerializeField]
    private Camera mainCamera;
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var targetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(targetPos.x, targetPos.y, 0);
        }
    }
}
