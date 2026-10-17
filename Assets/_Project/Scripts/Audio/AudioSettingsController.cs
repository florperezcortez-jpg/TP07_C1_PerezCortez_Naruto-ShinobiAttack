using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void SetMasterVolume(float value)
    {
        SetVolume("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        SetVolume("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        SetVolume("SFXVolume", value);
    }
    private void SetVolume (string parameter, float value)
    {
        if (audioMixer == null)
            return;

        float decibels = value <= 0.0001f
            ? -80f
            : Mathf.Log10(value) * 20f;

        audioMixer.SetFloat(parameter, decibels);
        PlayerPrefs.SetFloat(parameter, value);
    }

    private void OnDestroy()
    {
        if (masterSlider != null)
            masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        if(musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
