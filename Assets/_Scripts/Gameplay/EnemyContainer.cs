﻿using System;
using System.Collections.Generic;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.Turns;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class EnemyContainer : MonoBehaviour
    {
        private List<EnemyMarker> _enemies = new List<EnemyMarker>();

        public EnemyMarker[] Enemies => _enemies.ToArray();
        
        [Inject]
        private FightPlayer FightPlayer { get; set; }
        
        [Inject]
        private ContainerFactory ContainerFactory { get; set; }
        
        [Inject]
        private SpotMap SpotMap { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FightPlayer.PlayFight();
            }
        }

        public void SpawnEnemy(GameObject prefab, int spotIndex)
        {
            Spot targetSpot = SpotMap.GetSpot(spotIndex);
            if (targetSpot.IsOccupiedBy<CharacterMovement>())
                throw new InvalidOperationException($"Spot {spotIndex} is already occupied. Cannot spawn here");
            
            Vector3 position = targetSpot.transform.position;
            EnemyMarker newEnemy = ContainerFactory.Instantiate<EnemyMarker>(prefab, position, transform);
            _enemies.Add(newEnemy);
            newEnemy.Movement.AssignHomeSpot(spotIndex);
        }
    }
}