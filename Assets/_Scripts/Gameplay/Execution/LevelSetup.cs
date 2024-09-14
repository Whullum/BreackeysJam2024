using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Props;
using _Scripts.Gameplay.SpotSystem;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Execution
{
    public class LevelSetup : MonoBehaviour
    {
        [SerializeField]
        private EnemyMarker _enemyPrefab;

        [Inject]
        private PropFactory _propFactory;
        
        [Inject]
        private LevelLoader _levelLoader;
        
        [Inject]
        private EnemyContainer _enemyContainer;

        [Inject]
        private PlayerMarker _player;

        [Inject]
        private SoundManager _soundManager;

        private void Start()
        {
            SetupLevel(_levelLoader.CurrentLevel.Level);

            _soundManager.PlayBackgroundMusic(BackgroundMusicType.Simulation);
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
            _player.Movement.AssignOriginalDirection(currentLevel.PlayerReversed ? -1 : 1);

            foreach (EnemySpawnInfo spawnInfo in currentLevel.Enemies)
            {
                EnemyMarker newEnemy = _enemyContainer.SpawnEnemy(_enemyPrefab, new Vector2Int(spawnInfo.Spot, 0));
                newEnemy.Movement.AssignOriginalDirection(spawnInfo.Reversed ? -1 : 1);
                if (spawnInfo.BehaviourOverride.Length > 0)
                {
                    newEnemy.Behaviour.ApplyLoop(spawnInfo.BehaviourOverride);
                }
                if (spawnInfo.WeaponOverride != null)
                {
                    newEnemy.Attack.AssignStartingWeaponType(spawnInfo.WeaponOverride);
                }
            }

            foreach (PropsSpawnData prop in currentLevel.Props)
            {
                _propFactory.SpawnObject(prop.Prefab, new Vector2Int(prop.Spot, 0));
            }
        }
    }
}