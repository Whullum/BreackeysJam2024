using _Scripts.Gameplay.Execution;
using _Scripts.UI.Project;
using UnityEngine;
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

    [Inject]
    private MainMenuUISystem _uiSystem;
    
    [Inject]
    private ForesightUsage _foresightUsage;

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
        _uiSystem.OpenSettings();
        PlayClickSFX();
    }

    public void OnCreditsButtonClick()
    {
        _uiSystem.OpenCredits();
        PlayClickSFX();
    }

    private void OnExitButtonClick()
    {
        PlayClickSFX();
        Application.Quit();
    }

    private void LoadGameplay()
    {
        _levelLoader.LoadLevel(0);
        _foresightUsage.ResetUses();
    }

    private void PlayClickSFX()
    {
        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
    }
}
