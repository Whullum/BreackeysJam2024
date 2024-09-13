using _Scripts.Gameplay.Execution;
using Zenject;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private SoundManager _soundManager;

        [SerializeField]
        private LevelLoader _levelLoader;
        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle();
        }
    }
}
