using System;
using System.Collections.Generic;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Execution;
using _Scripts.Gameplay.SpotSystem;
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
        private FightPlayer _fightPlayer;
        
        [Inject]
        private ContainerFactory _containerFactory;
        
        [Inject]
        private SpotMap SpotMap { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fightPlayer.PlayFight();
            }
        }

        public EnemyMarker SpawnEnemy(EnemyMarker prefab, Vector2Int coordinates)
        {
            Spot targetSpot = SpotMap.GetSpot(coordinates);
            if (targetSpot.IsOccupiedBy<CharacterMovement>())
                throw new InvalidOperationException($"Spot {coordinates} is already occupied. Cannot spawn here");
            
            Vector3 position = targetSpot.transform.position;
            EnemyMarker newEnemy = _containerFactory.Instantiate<EnemyMarker>(prefab, position, transform);
            _enemies.Add(newEnemy);
            newEnemy.Movement.AssignHomeSpot(coordinates);
            return newEnemy;
        }
    }
}