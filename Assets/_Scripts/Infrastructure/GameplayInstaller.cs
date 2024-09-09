using _Scripts.Gameplay;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private Timeline _timeline;
        
        public override void InstallBindings()
        {
            Container.Bind<Timeline>().FromInstance(_timeline).AsSingle();
        }
    }
}