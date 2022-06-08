using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioClip soundExamle;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        var slider = GetComponent<Slider>();
        audioMixer.GetFloat("SoundsVolume", out float volume);
        float sliderValue = (volume + 80) / 80 * 12;
        slider.value = sliderValue;
        Debug.Log("Sounds volume = " + volume + ", slider = " + sliderValue);
    }

    public void SetSoundsVolume(float sliderValue)
    {
        float newVolume = sliderValue / 12 * 80 - 80;
        audioMixer.SetFloat("SoundsVolume", newVolume);
        audioSource.PlayOneShot(soundExamle);
        Debug.Log("Sounds volume = " + newVolume);
    }
}
