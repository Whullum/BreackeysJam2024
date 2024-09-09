using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<SpotManager>().AsSingle();
        }
    }
}