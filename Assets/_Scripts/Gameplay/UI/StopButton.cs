using System;
using _Scripts.Gameplay.Execution;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class StopButton : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [Inject]
        private FightPlayer _fightPlayer;
        [Inject]
        private SoundManager _soundManager;

        public void Stop()
        {
           _fightPlayer.StopFight(); 
            _soundManager.PlayButtonClickSFX(ButtonClickSFX.Default);
        }
        
        private void Update()
        {
            _canvasGroup.alpha = _fightPlayer.IsForeseeing ? 1 : 0;
            _canvasGroup.blocksRaycasts = _fightPlayer.IsForeseeing;

        }
    }
}