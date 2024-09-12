using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Turns
{
    public class LevelSetup : MonoBehaviour
    {
        [Inject]
        private LevelLoader _levelLoader;


        [SerializeField]
        private EnemyMarker _enemyPrefab;

        [Inject]
        private EnemyContainer _enemyContainer { get; set; }

        [Inject]
        private PlayerMarker _player { get; set; }

        private void Start()
        {
            SetupLevel(_levelLoader.CurrentLevel);
        }

        private void SetupLevel(LevelData currentLevel)
        {
            if (currentLevel == null)
            {
                Debug.LogError("Failed to load level.");
                return;
            }
            if (!currentLevel)
            {
                Debug.LogError("Failed to load level.");
                return;
            }
            
            _player.Movement.AssignHomeSpot(new Vector2Int(currentLevel.PlayerSpot, 0));

            foreach (EnemySpawnInfo spawnInfo in currentLevel.Enemies)
            {
                EnemyMarker newEnemy = _enemyContainer.SpawnEnemy(_enemyPrefab, new Vector2Int(spawnInfo.Spot, 0));
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