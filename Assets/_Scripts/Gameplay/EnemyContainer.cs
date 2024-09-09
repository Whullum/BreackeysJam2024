using System;
using System.Collections.Generic;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Turns;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class EnemyContainer : MonoBehaviour
    {
        [SerializeField] // For now it's seriazlized but enemies must be spawned and filled here
        private List<EnemyMarker> _enemies;

        public EnemyMarker[] Enemies => _enemies.ToArray();
        
        [Inject]
        private TurnsSystem TurnsSystem { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TurnsSystem.BuildTurnsSequence();
            }
        }
    }
}