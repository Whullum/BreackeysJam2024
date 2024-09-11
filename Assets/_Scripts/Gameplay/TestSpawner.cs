using System;
using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class TestSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2Int _playerSpot;
        
        [SerializeField]
        private SpawnInfo[] _spawnList;

        [Inject]
        private EnemyContainer EnemyContainer { get; set; }
        
        [Inject]
        private PlayerMarker Player { get; set; }
        
        private void Start()
        {
            Player.Movement.AssignHomeSpot(_playerSpot);
            
            foreach (SpawnInfo spawnInfo in _spawnList)
            {
                EnemyContainer.SpawnEnemy(spawnInfo.Prefab, spawnInfo.Spot);
            }
        }
    }
}