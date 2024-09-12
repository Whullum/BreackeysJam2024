using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.Turns;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuButton : MonoBehaviour
{
    private Button _button;

    [Inject]
    private LevelLoader _levelLoader;

    // Start is called before the first frame update
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        _levelLoader.LoadMainMenu();
    }
}
