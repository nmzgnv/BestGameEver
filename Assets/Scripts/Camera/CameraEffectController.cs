using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class CameraEffectController : MonoBehaviour
{
    [SerializeField]
    private float shakeSmooth = 5;

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

    private IEnumerator Shake(float secondsDuration, Vector2 force)
    {
        var elapsedTime = 0f;
        while (elapsedTime < secondsDuration)
        {
            var originalPos = transform.position;
            var newPosition = transform.position + new Vector3(force.x, force.y, originalPos.z);
            var lerpPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * shakeSmooth);
            transform.position = lerpPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}