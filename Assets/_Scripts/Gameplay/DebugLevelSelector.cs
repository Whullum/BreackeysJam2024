    using System;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Execution;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class DebugLevelSelector : MonoBehaviour
    {        
        [Inject]
        private LevelLoader _levelLoader;
        
        [SerializeField]
        private int _levelID;

        [Button]
        private void LoadSelectedLevel()
        {
            _levelLoader.LoadLevel(_levelID);
        }
    }
}