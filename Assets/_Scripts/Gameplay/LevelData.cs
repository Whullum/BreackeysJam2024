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
        private WeaponType _playerStartingWeapon;
        
        [SerializeField]
        private EnemySpawnInfo[] _enemies;

        [SerializeField]
        private PropsSpawnData[] _props;

        [SerializeField]
        private bool _isTutorialLevel = false;
        
        [NaughtyAttributes.ResizableTextArea]
        [SerializeField]
        private string _tutorialText = "";

        public int PlayerSpot => _playerSpot;

        public bool PlayerReversed => _playerReversed;

        public WeaponType PlayerStartingWeapon => _playerStartingWeapon;

        public EnemySpawnInfo[] Enemies => _enemies;

        public PropsSpawnData[] Props => _props;

        public override LevelData Level => this;

        public bool IsTutorialLevel => _isTutorialLevel;

        public string TutorialText => _tutorialText;
    }
}