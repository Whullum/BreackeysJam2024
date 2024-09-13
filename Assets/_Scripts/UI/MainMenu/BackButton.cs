using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackButton : MonoBehaviour
{
    [Inject]
    private MainMenuUISystem _uiSystem;

    [SerializeField]
    private Button _button;

    [Inject]
    private SoundManager _soundManager;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
        _uiSystem.OpenMainMenu();
    }
}
