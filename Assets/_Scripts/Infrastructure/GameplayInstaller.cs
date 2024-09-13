using _Scripts.Gameplay;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Execution;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.UI;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerMarker _playerMarker;
        
        [SerializeField]
        private Timeline _timeline;
        
        [SerializeField]
        private EnemyContainer _enemyContainer;
        
        [SerializeField]
        private SpotMap _spotMap;
        
        [SerializeField]
        private WeaponOnGroundFactory _weaponOnGroundFactory;

        [SerializeField]
        private ManaBarController _manaBarController;

        [SerializeField]
        private FightPlayer _fightPlayer;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerMarker>().FromInstance(_playerMarker);
            Container.Bind<Timeline>().FromInstance(_timeline).AsSingle();
            Container.Bind<SpotMap>().FromInstance(_spotMap).AsSingle();
            Container.Bind<EnemyContainer>().FromInstance(_enemyContainer).AsSingle();
            Container.Bind<FightPlayer>().FromInstance(_fightPlayer).AsSingle();
            Container.Bind<WeaponOnGroundFactory>().FromInstance(_weaponOnGroundFactory).AsSingle();
            Container.Bind<ManaBarController>().FromInstance(_manaBarController).AsSingle();
            Container.Bind<ComboSystem>().AsSingle();
        }
    }
}