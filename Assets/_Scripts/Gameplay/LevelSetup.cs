using System;
using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class LevelSetup : MonoBehaviour
    {
        [SerializeField]
        private LevelData _currentLevel;
        
        [SerializeField]
        private GameObject _enemyPrefab;
        
        [Inject]
        private EnemyContainer EnemyContainer { get; set; }
        
        [Inject]
        private PlayerMarker Player { get; set; }
        
        private void Start()
        {
            Player.Movement.AssignHomeSpot(new Vector2Int(_currentLevel.PlayerSpot, 0));
            
            foreach (EnemySpawnInfo spawnInfo in _currentLevel.Enemies)
            {
                EnemyMarker newEnemy = EnemyContainer.SpawnEnemy(_enemyPrefab, new Vector2Int(spawnInfo.Spot, 0));
                if (spawnInfo.BehaviourOverride.Length > 0)
                {
                    newEnemy.Behaviour.ApplyLoop(spawnInfo.BehaviourOverride);
                }
                if (spawnInfo.WeaponOverride != null)
                {
                    newEnemy.Attack.AssignStartingWeaponType(spawnInfo.WeaponOverride);
                }
            }
        }
    }
}