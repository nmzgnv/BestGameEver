using UnityEngine;
using Random = UnityEngine.Random;

public class TeleportationSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip teleportDown;

    [SerializeField]
    private AudioClip teleportFailedSound;

    [SerializeField]
    private TeleportationScript teleportation;

    private void Start()
    {
        teleportation.OnTeleportDown += () =>
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(teleportDown, 0.4f);
        };
        teleportation.OnTeleportFailed += () =>
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(teleportFailedSound, 1.4f);
        };
    }
}