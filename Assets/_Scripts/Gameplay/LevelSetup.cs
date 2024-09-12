﻿    using System;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Turns;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class LevelSetup : MonoBehaviour
    {        
        [Inject]
        private LevelLoader _levelLoader;
        
        [SerializeField]
        [Range(0,3)]
        private int _levelID;

        [Button]
        private void LoadSelectedLevel()
        {
            _levelLoader.LoadLevel(_levelID);
        }
    }
}