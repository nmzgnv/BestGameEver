using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(2, 10, 0);

    [SerializeField]
    private float smooth;

    public void Update()
    {
        var newPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
        transform.position = newPosition;
    }
}