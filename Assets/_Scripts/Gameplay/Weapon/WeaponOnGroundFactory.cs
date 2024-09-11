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
        private ContainerFactory ContainerFactory  { get; set; }

        [Inject]
        private SpotMap SpotMap { get; set; }
        
        public void SpawnWeapon(WeaponType type, Vector2Int coordinates)
        {
            Spot spot = SpotMap.GetSpot(coordinates);
            if (spot == null)
                return;
            
            spot.ForceLeave<WeaponOnGround>();
            WeaponOnGround newWeapon = ContainerFactory.Instantiate<WeaponOnGround>(_prefab, spot.transform.position, transform);
            newWeapon.GoToSpot(coordinates, true);
            newWeapon.Init(type);
        }
    }
}