using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.Execution;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuButton : MonoBehaviour
{
    private Button _button;

    [Inject]
    private LevelLoader _levelLoader;

    [Inject]
    private SoundManager _soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {

        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
        _levelLoader.LoadMainMenu();
    }
}
