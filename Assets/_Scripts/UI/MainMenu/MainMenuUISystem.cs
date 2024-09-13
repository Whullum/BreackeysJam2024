using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuUISystem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _menus;

    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _settings;
    [SerializeField]
    private GameObject _credits;

    [Inject]
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager.StopRepeatAudio();
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        CloseAllMenus();
        _mainMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        CloseAllMenus();
        _settings.SetActive(true);
    }

    public void OpenCredits()
    {
        CloseAllMenus();
        _credits.SetActive(true);
    }

    private void CloseAllMenus()
    {
        foreach (var menu in _menus)
        {
            menu.SetActive(false);
        }
    }
}
