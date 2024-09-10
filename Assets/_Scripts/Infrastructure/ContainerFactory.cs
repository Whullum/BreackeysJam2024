using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure
{
    public class ContainerFactory : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ContainerFactory>().FromInstance(this).AsSingle();
        }
        
        public T Instantiate<T>(GameObject prefab, Vector3 position, Transform parent = null) where T : Component
            => Container.InstantiatePrefab(prefab, position, Quaternion.identity, parent).GetComponent<T>(); 
        
        public T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Component
            => Container.InstantiatePrefab(prefab, parent).GetComponent<T>(); 
    }
}