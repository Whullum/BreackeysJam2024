using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsController : MonoBehaviour
{

    [SerializeField]
    private Slider _mainVolumeSlider;


    [SerializeField]
    private Slider _musicVolumeSlider;


    [SerializeField]
    private Slider _sfxVolumeSlider;

    [Inject]
    private SoundManager _soundManager;


    private string _mainVolumeKey = "Volume/Main";
    private string _sfxVolumeKey = "Volume/SFX";
    private string _musicVolumeKey = "Volume/Music";

    private void OnEnable()
    {
        float mainVolume = GetSavedVolume(_mainVolumeKey);
        _mainVolumeSlider.value = mainVolume;
        float sfxVolume = GetSavedVolume(_sfxVolumeKey);
        _sfxVolumeSlider.value = sfxVolume;
        float musicVolume = GetSavedVolume(_musicVolumeKey);
        _musicVolumeSlider.value = musicVolume;

        _mainVolumeSlider.onValueChanged.AddListener(UpdateMainVolume);
        _sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
        _musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
    }

    public void UpdateMainVolume(float value)
    {
        _mainVolumeSlider.value = value;
        _soundManager.ChangeVolume(VolumeType.Main, value);
        PlaySoundEffect();
    }

    public void UpdateSFXVolume(float value)
    {
        _sfxVolumeSlider.value = value;
        _soundManager.ChangeVolume(VolumeType.SFX, value);
        PlaySoundEffect();
    }

    public void UpdateMusicVolume(float value)
    {
        _musicVolumeSlider.value = value;
        _soundManager.ChangeVolume(VolumeType.Music, value);
        PlaySoundEffect();
    }

    private void OnDisable()
    {
        SaveSettings();
    }

    private float GetSavedVolume(string key)
    {
        return PlayerPrefs.GetFloat(key, 1f);
    }

    private void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    private void PlaySoundEffect()
    {
        _soundManager.PlayButtonHoverSFX(ButtonHoverSFX.Card);
    }
}
