using Zenject;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private SoundManager _soundManager;
        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
        }
    }
}
