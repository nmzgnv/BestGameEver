using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ImprovementBase : MonoBehaviour
{
    [SerializeField]
    private AudioClip applySound;

    protected virtual void ApplyImprovement(Player player)
    {
        if (applySound == null)
            return;
        var audioSource = player.AudioSource;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(applySound);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
            ApplyImprovement(player);

        Destroy(gameObject);
    }
}