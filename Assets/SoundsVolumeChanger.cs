using UnityEngine;
using UnityEngine.Audio;

public class SoundsVolumeChanger : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("SoundsVolume", volume * 80 / 12 - 80); 
    }
}
