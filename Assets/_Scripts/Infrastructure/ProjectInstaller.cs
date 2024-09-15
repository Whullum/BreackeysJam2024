using _Scripts.Gameplay;
using _Scripts.Gameplay.Execution;
using _Scripts.UI.Project;
using Zenject;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private SoundManager _soundManager;

        [SerializeField]
        private LevelLoader _levelLoaderPrefab;
        
        [SerializeField]
        private ForesightUsage _foresightUsagePrefab;
        
        [SerializeField]
        private TutorialBank _tutorialBankPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();

            LevelLoader levelLoader = Container.InstantiatePrefab(_levelLoaderPrefab, transform).GetComponent<LevelLoader>();
            Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle(); 
            
            ForesightUsage foresightUsage = Container.InstantiatePrefab(_foresightUsagePrefab, transform).GetComponent<ForesightUsage>();
            Container.Bind<ForesightUsage>().FromInstance(foresightUsage).AsSingle(); 
            
            TutorialBank tutorialBank = Container.InstantiatePrefab(_tutorialBankPrefab, transform).GetComponent<TutorialBank>();
            Container.Bind<TutorialBank>().FromInstance(tutorialBank).AsSingle(); 
        }
    }
}
