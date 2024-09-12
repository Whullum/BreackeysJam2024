using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public enum BackgroundMusicType
{
    None, MainMenu, Simulation, Fight
}

public enum ButtonClickSFX
{
    Default, Card, Simulation, Fight
}

public enum ButtonHoverSFX
{
    Card, Simulation, Fight
}

public enum CardPlayedSFX
{
    Disarm, Dodge, Kick, Punch, Step, Swap, Turn, Use, Wait
}

public enum VolumeType
{
    Main, SFX, Music
}

public enum GameplayAudioState
{
    Calm, Storm,
}

public class SoundManager : MonoBehaviour
{
    #region structs

    [Serializable]
    struct MusicSelector
    {
        public BackgroundMusicType type;
        public EventReference sound;
    }

    [Serializable]
    struct ButtonClickSFXSelector
    {
        public ButtonClickSFX type;
        public EventReference sound;
    }

    [Serializable]
    struct ButtonHoverSFXSelector
    {
        public ButtonHoverSFX type;
        public EventReference sound;
    }

    [Serializable]
    struct CardSFXSelector
    {
        public CardPlayedSFX type;
        public EventReference sound;
    }
    #endregion

    [SerializeField]
    private List<MusicSelector> _music;

    [SerializeField]
    private List<ButtonClickSFXSelector> _buttonClickSFX;

    [SerializeField]
    private List<ButtonHoverSFXSelector> _buttonHoverSFX;

    [SerializeField]
    private List<CardSFXSelector> _cardPlayedSFX;

    private EventReference _sliderSound;

    private FMOD.Studio.EventInstance _musicInstance;


    public void ChangeVolume(VolumeType volumeType, float volume)
    {
        string volumeTypeName = Enum.GetName(typeof(VolumeType), volumeType);

        // insert playerprefs fuckery maybe

        string busName = "bus:/" + volumeTypeName;
        var bus = FMODUnity.RuntimeManager.GetBus(busName);
        bus.setVolume(volume);
    }

    private void PlayOneShot(EventReference sound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(sound);
    }

    private void PlayOnRepeat(EventReference music)
    {
        // Will! Change this to repeat!!!!
        //PlayOneShot(music);

        _musicInstance = FMODUnity.RuntimeManager.CreateInstance(music);
        _musicInstance.start();
    }

    private void StopRepeatAudio()
    {
        _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicInstance.release();
    }

    private void SwitchMusicState(GameplayAudioState audioState)
    {
        if (audioState == GameplayAudioState.Calm)
            _musicInstance.setParameterByNameWithLabel("GameplayState", "Calm");
        else
            _musicInstance.setParameterByNameWithLabel("GameplayState", "Storm");
    }

    public void PlayBackgroundMusic(BackgroundMusicType musicType)
    {
        foreach (var music in _music)
        {
            if (music.type == musicType)
            {
                PlayOnRepeat(music.sound);
            }
        }
    }

    public void PlaySliderSound()
    {
        PlayOneShot(_sliderSound);
    }


    public void PlayButtonClickSFX(ButtonClickSFX buttonType)
    {
        foreach (var sound in _buttonClickSFX)
        {
            if (sound.type == buttonType)
            {
                PlayOneShot(sound.sound);
            }
        }
    }

    public void PlayButtonHoverSFX(ButtonHoverSFX buttonType)
    {
        foreach (var sound in _buttonHoverSFX)
        {
            if (sound.type == buttonType)
            {
                PlayOneShot(sound.sound);
            }
        }
    }

    public void CardPlayedSFX(CardPlayedSFX cardType)
    {
        foreach (var sound in _cardPlayedSFX)
        {
            if (sound.type == cardType)
            {
                PlayOneShot(sound.sound);
            }
        }
    }
}
