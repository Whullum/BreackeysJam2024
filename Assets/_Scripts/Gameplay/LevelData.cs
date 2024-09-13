using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Props;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Gameplay
{
    [CreateAssetMenu(menuName = "Level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _playerSpot;
        
        [SerializeField]
        private EnemySpawnInfo[] _enemies;

        [FormerlySerializedAs("_otherObjects")] [SerializeField]
        private PropsSpawnData[] _props;
        
        public int PlayerSpot => _playerSpot;

        public EnemySpawnInfo[] Enemies => _enemies;

        public PropsSpawnData[] Props => _props;
    }
}