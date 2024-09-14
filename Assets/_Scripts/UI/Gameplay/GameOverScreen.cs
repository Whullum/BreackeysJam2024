using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.Execution;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _toEnable;

    [SerializeField]
    private Button _restartButton;

    [Inject]
    private LevelLoader _levelLoader;

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestart);
    }

    private void OnRestart()
    {
        _levelLoader.LoadLevel(0);
    }

    public void ShowGameOver()
    {
        _toEnable.SetActive(true);
    }

}
