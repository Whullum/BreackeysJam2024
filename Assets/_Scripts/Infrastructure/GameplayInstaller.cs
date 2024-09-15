using System.Reflection;
using _Scripts.Gameplay;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Props;
using _Scripts.Gameplay.Execution;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.UI;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using System;
using NaughtyAttributes.Test;

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
        private PropFactory _propFactory;

        [SerializeField]
        private ManaBarController _manaBarController;

        [SerializeField]
        private FightPlayer _fightPlayer;

        [SerializeField]
        private TutorialScript _tutorial;

        [SerializeField]
        private GameOverScreen _gameOverScreen;

        [SerializeField]
        private VictoryScreen _victoryScreen;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerMarker>().FromInstance(_playerMarker);
            Container.Bind<Timeline>().FromInstance(_timeline).AsSingle();
            Container.Bind<SpotMap>().FromInstance(_spotMap).AsSingle();
            Container.Bind<EnemyContainer>().FromInstance(_enemyContainer).AsSingle();
            Container.Bind<FightPlayer>().FromInstance(_fightPlayer).AsSingle();
            Container.Bind<WeaponOnGroundFactory>().FromInstance(_weaponOnGroundFactory).AsSingle();
            Container.Bind<PropFactory>().FromInstance(_propFactory).AsSingle();
            Container.Bind<ManaBarController>().FromInstance(_manaBarController).AsSingle();
            Container.Bind<ComboSystem>().AsSingle();
            Container.Bind<TutorialScript>().FromInstance(_tutorial).AsSingle();
            Container.Bind<GameOverScreen>().FromInstance(_gameOverScreen).AsSingle();
            Container.Bind<VictoryScreen>().FromInstance(_victoryScreen).AsSingle();
        }
    }
}