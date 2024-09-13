using System.Collections.Generic;
using System.Linq;
using _Scripts.Extentions;
using _Scripts.Gameplay.Execution;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Weapon
{
    public class WeaponOnGroundFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        
        [Inject]
        private ContainerFactory _containerFactory;

        private List<WeaponOnGround> _pool = new List<WeaponOnGround>();

        public List<WeaponOnGround> Pool
        {
            get
            {
                _pool = _pool.Where(w => w != null).ToList();
                return _pool;
            }
        }

        [Inject]
        private SpotMap SpotMap { get; set; }
        
        public void SpawnWeapon(WeaponType type, Vector2Int coordinates)
        {
            Spot spot = SpotMap.GetSpot(coordinates);
            if (spot == null)
                return;
            
            spot.ForceLeave<WeaponOnGround>();
            WeaponOnGround newWeapon = _containerFactory.Instantiate<WeaponOnGround>(_prefab, spot.transform.position, transform);
            newWeapon.GoToSpot(coordinates, true);
            newWeapon.Init(type);
            _pool.Add(newWeapon);
        }

        public void Discard()
        {
            _pool.Select(w => w.GetComponent<IDiscardable>()).Foreach(d => d.Discard());
        }
    }
}