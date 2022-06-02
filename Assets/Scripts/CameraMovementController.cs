using System.Collections;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -10);

    [SerializeField]
    private float smooth = 5;

    [SerializeField]
    private float shakeMagnitude = 0.7f;

    [SerializeField]
    private float shakeDuration = .1f;

    private Player _player;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player != null)
            _player.Attack.OnPlayerAttacks += () =>
            {
                StartCoroutine(
                    Shake(shakeDuration, _player.PhysicsMovement.LastViewDirection * shakeMagnitude));
            };
    }

    private float GetRandomMagnitude(float magnitude)
    {
        return Random.Range(-1f, 1f) * magnitude;
    }

    private IEnumerator Shake(float secondsDuration, Vector2 force)
    {
        var elapsedTime = 0f;
        while (elapsedTime < secondsDuration)
        {
            var originalPos = transform.position;
            var newPosition = transform.position + new Vector3(force.x, force.y, originalPos.z);
            var lerpedPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smooth);
            transform.position = lerpedPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void Update()
    {
        if (target == null)
            return;
        var newPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
        transform.position = newPosition;
    }
}