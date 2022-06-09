using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private void Start()
    {
        var slider = GetComponent<Slider>();
        audioMixer.GetFloat("MusicVolume", out float volume);
        float sliderValue = (volume + 80) / 80 * 12;
        slider.value = sliderValue;
        Debug.Log("Music volume = " + volume + ", slider = " + sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        float newVolume = sliderValue / 12 * 80 - 80;
        audioMixer.SetFloat("MusicVolume", newVolume);
        Debug.Log("Music volume = " + newVolume);
    }
}
