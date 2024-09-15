using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _tutorialText;

    [Inject]
    private SoundManager _soundManager;

    public void ActivateTutorial()
    {
        gameObject.SetActive(true);
    }

    public void SetTutorialText(string text)
    {
        _tutorialText.text = text;
    }

    public void DeActivateTutorial()
    {
        gameObject.SetActive(false);
    }

    public void PlayButtonClick()
    {
        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
    }
}
