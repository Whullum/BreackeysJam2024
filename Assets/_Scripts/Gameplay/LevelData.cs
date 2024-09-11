using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Weapon;
using UnityEngine;

namespace _Scripts.Gameplay
{
    [CreateAssetMenu(menuName = "Level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _playerSpot;
        
        [SerializeField]
        private EnemySpawnInfo[] _enemies;

        public int PlayerSpot => _playerSpot;

        public EnemySpawnInfo[] Enemies => _enemies;
    }
}