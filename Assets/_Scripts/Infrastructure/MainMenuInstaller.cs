using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {   

        [SerializeField]
        private MainMenuUISystem _uiSystem;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuUISystem>().FromInstance(_uiSystem).AsSingle();
        }
    }
}