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

    [SerializeField]
    private float _timeBetweenSounds = 0.1f;
    private float _lastSoundPlayed = 0f;
    private bool _playSound =false;

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
        PlayerPrefs.SetFloat(_mainVolumeKey, value);
        _soundManager.ChangeVolume(VolumeType.Main, value);
        _playSound = true;
    }

    public void UpdateSFXVolume(float value)
    {
        _sfxVolumeSlider.value = value;
        PlayerPrefs.SetFloat(_sfxVolumeKey, value);
        _soundManager.ChangeVolume(VolumeType.SFX, value);
        _playSound = true;
    }

    public void UpdateMusicVolume(float value)
    {
        _musicVolumeSlider.value = value;
        PlayerPrefs.SetFloat(_musicVolumeKey, value);
        _soundManager.ChangeVolume(VolumeType.Music, value);
        _playSound = true;
    }

    private void Update()
    {
        if (Time.time - _lastSoundPlayed > _timeBetweenSounds)
        {
            if (_playSound)
            {
                PlaySoundEffect();
                _playSound = false;
                _lastSoundPlayed = Time.time;
            }
        }
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
