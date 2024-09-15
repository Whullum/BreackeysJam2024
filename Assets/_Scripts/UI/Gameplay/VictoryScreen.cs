using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.Execution;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VictoryScreen : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _toEnable;

    [SerializeField]
    private Button _nextLevelButton;

    [Inject]
    private LevelLoader _levelLoader;

    [Inject]
    private SoundManager _soundManager;

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _levelLoader.LoadNextLevel();
    }

    public void ShowVictoryScreen()
    {
        _toEnable.SetActive(true);
        _soundManager.PlayButtonClickSFX(ButtonClickSFX.Victory);
    }
}
