using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Props;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Gameplay
{
    [CreateAssetMenu(menuName = "Level")]
    public class LevelData : LevelSource
    {
        [SerializeField]
        private int _playerSpot;

        [SerializeField]
        private bool _playerReversed;
        
        [SerializeField]
        private EnemySpawnInfo[] _enemies;

        [SerializeField]
        private PropsSpawnData[] _props;
        
        public int PlayerSpot => _playerSpot;

        public bool PlayerReversed => _playerReversed;

        public EnemySpawnInfo[] Enemies => _enemies;

        public PropsSpawnData[] Props => _props;

        public override LevelData Level => this;
    }
}