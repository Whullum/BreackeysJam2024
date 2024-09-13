using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Execution
{
    public class LevelSetup : MonoBehaviour
    {
        [SerializeField]
        private EnemyMarker _enemyPrefab;

        [Inject]
        private LevelLoader _levelLoader;

        [Inject]
        private EnemyContainer _enemyContainer;

        [Inject]
        private PlayerMarker _player;

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