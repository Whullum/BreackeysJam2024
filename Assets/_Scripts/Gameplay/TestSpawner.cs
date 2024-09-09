using System;
using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class TestSpawner : MonoBehaviour
    {
        [SerializeField]
        private SpawnInfo[] _spawnList;

        [Inject]
        private EnemyContainer EnemyContainer { get; set; }
        
        private void Start()
        {
            foreach (SpawnInfo spawnInfo in _spawnList)
            {
                EnemyContainer.SpawnEnemy(spawnInfo.Prefab, spawnInfo.Spot);
            }
        }
    }
}