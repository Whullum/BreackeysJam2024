using System.Collections;
using System.Collections.Generic;
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
        LoadGameplay();
        PlayClickSFX();
        
    }

    private void OnSettingsButtonClick()
    {
        _settingsButton.interactable = false;
    }

    public void OnCreditsButtonClick()
    {
        _creditsButton.interactable = false;
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void LoadGameplay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void PlayClickSFX()
    {
        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
    }
}
