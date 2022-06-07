using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PostProcessVolume))]
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

    [SerializeField]
    private float damageVignetteIntensity = 0.513f;

    [SerializeField]
    private float damageVignetteSmoothness = 0.361f;

    [SerializeField]
    private Color damageColor = Color.red;

    private Player _player;
    private Vignette _vignetteEffect;
    private Color _defaultVignetteColor;
    private float _defaultDamageVignetteIntensity;
    private float _defaultDamageVignetteSmoothness;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private void Awake()
    {
        var postProcessVolume = GetComponent<PostProcessVolume>();
        if (postProcessVolume.profile.TryGetSettings(out _vignetteEffect))
        {
            _defaultVignetteColor = _vignetteEffect.color.value;
            _defaultDamageVignetteIntensity = _vignetteEffect.intensity.value;
            _defaultDamageVignetteSmoothness = _vignetteEffect.smoothness.value;
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player != null)
        {
            _player.Attack.OnPlayerAttacks += () =>
            {
                StartCoroutine(
                    Shake(shakeDuration, _player.PhysicsMovement.LastViewDirection * shakeMagnitude));
            };
            if (_vignetteEffect != null)
                _player.Health.OnPlayerTakesDamage += () => { StartCoroutine(PlayTakeDamageEffect()); };
        }
    }

    private IEnumerator PlayTakeDamageEffect()
    {
        if (_vignetteEffect == null)
            yield break;

        _vignetteEffect.color.value = damageColor;
        _vignetteEffect.intensity.value = damageVignetteIntensity;
        _vignetteEffect.smoothness.value = damageVignetteSmoothness;
        yield return new WaitForSeconds(0.2f);
        _vignetteEffect.color.value = _defaultVignetteColor;
        _vignetteEffect.intensity.value = _defaultDamageVignetteIntensity;
        _vignetteEffect.smoothness.value = _defaultDamageVignetteSmoothness;
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