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

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
            _uiSystem.OpenMainMenu();
    }
}
