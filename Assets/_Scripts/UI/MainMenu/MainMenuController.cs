using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.Turns;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenuController : MonoBehaviour
{

    [SerializeField]
    private Button _startGameButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _creditsButton;
    [SerializeField]
    private Button _exitButton;

    [Inject]
    private LevelLoader _levelLoader;

    [Inject]
    private SoundManager _soundManager;

    void Awake()
    {
        _startGameButton.onClick.AddListener(OnStartButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _creditsButton.onClick.AddListener(OnCreditsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public void OnStartButtonClick()
    {
        PlayClickSFX();
        LoadGameplay();
        
    }

    private void OnSettingsButtonClick()
    {
        PlayClickSFX();
        _settingsButton.interactable = false;
    }

    public void OnCreditsButtonClick()
    {
        PlayClickSFX();
        _creditsButton.interactable = false;
    }

    private void OnExitButtonClick()
    {
        PlayClickSFX();
        Application.Quit();
    }

    private void LoadGameplay()
    {
        _levelLoader.LoadLevel(0);
    }

    private void PlayClickSFX()
    {
        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
    }
}
