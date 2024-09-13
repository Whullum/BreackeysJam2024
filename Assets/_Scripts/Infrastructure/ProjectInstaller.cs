using _Scripts.Gameplay.Execution;
using Zenject;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private SoundManager _soundManager;

        [SerializeField]
        private LevelLoader _levelLoaderPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();

            LevelLoader levelLoader = Container.InstantiatePrefab(_levelLoaderPrefab, transform).GetComponent<LevelLoader>();
            Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle();
        }
    }
}
