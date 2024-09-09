﻿using _Scripts.Gameplay;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.Turns;
using _Scripts.Gameplay.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private Timeline _timeline;
        
        [SerializeField]
        private EnemyContainer _enemyContainer;
        
        [SerializeField]
        private SpotMap spotMap;
        
        public override void InstallBindings()
        {
            Container.Bind<Timeline>().FromInstance(_timeline).AsSingle();
            Container.Bind<SpotMap>().FromInstance(spotMap).AsSingle();
            Container.Bind<EnemyContainer>().FromInstance(_enemyContainer).AsSingle();
            Container.Bind<TurnsSystem>().AsSingle();
        }
    }
}