using System.Collections.Generic;
using System.Linq;
using _Scripts.Extentions;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.SpotSystem
{
    public class SpotObjectFactory<T> : MonoBehaviour where T : SpotObject
    {
        [SerializeField]
        private T _defaultPrefab;
        
        [Inject]
        private ContainerFactory ContainerFactory  { get; set; }

        private List<T> _pool = new List<T>();

        public List<T> Pool
        {
            get
            {
                _pool = _pool.Where(w => w != null).ToList();
                return _pool;
            }
        }

        [Inject]
        private SpotMap SpotMap { get; set; }
        
        public T SpawnObject(GameObject prefab, Vector2Int coordinates)
        {
            Spot spot = SpotMap.GetSpot(coordinates);
            if (spot == null)
                return null;
            
            T newObj = ContainerFactory.Instantiate<T>(prefab ?? _defaultPrefab.gameObject, spot.transform.position, transform);
            newObj.GoToSpot(coordinates, true);
            _pool.Add(newObj);
            return newObj;
        }

        public void Discard()
        {
            _pool.Select(w => w.GetComponent<IDiscardable>()).Foreach(d => d.Discard());
        }
    }
}