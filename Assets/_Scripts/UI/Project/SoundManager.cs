using System.Collections;
using System.Collections.Generic;
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

public class SoundManager : MonoBehaviour
{
    
    public void PlayBackgroundMusic(BackgroundMusicType musicType)
    {

    }

    public void PlaySliderSound()
    {

    }


    public void PlayButtonClickSFX(ButtonClickSFX buttonType)
    {

    }

    public void PlayButtonHoverSFX(ButtonHoverSFX buttonType)
    {
        
    }

}
