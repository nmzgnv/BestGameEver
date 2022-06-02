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
    private float shakeMagnitude = 0.001f;

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
                var magnitude = (1 / (_player.PhysicsMovement.Speed + 0.001f)) * shakeMagnitude;
                StartCoroutine(Shake(shakeDuration, magnitude));
            };
    }

    private float GetRandomMagnitude(float magnitude)
    {
        return Random.Range(-1f, 1f) * magnitude;
    }

    private IEnumerator Shake(float secondsDuration, float magnitude)
    {
        var originalPos = transform.position;
        var elapsedTime = 0f;
        while (elapsedTime < secondsDuration)
        {
            var x = GetRandomMagnitude(magnitude);
            var y = GetRandomMagnitude(magnitude);

            transform.localPosition += new Vector3(x, y, originalPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

    public void Update()
    {
        if (target == null)
            return;
        var newPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
        transform.position = newPosition;
    }
}