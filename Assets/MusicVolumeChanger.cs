using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeChanger : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float sliderValue)
    {
        var volume = sliderValue * 80 / 12 - 80;
        audioMixer.SetFloat("MusicVolume", volume);
        Debug.Log(volume);
    }
}
