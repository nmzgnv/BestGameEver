using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -10);

    [SerializeField]
    private float smooth = 5;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public void Update()
    {
        if (target == null)
            return;
        var newPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
        transform.position = newPosition;
    }
}