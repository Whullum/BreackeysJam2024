using _Scripts.Gameplay.SpotSystem;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterAttack : MonoBehaviour, IRestorable
    {
        [SerializeField]
        private WeaponType _startingWeaponType;
        
        private WeaponType _currentWeaponType;

        public WeaponType CurrentWeaponType => _currentWeaponType;
        
        [SerializeField]
        private int _punchDamage;
        
        [SerializeField]
        private int _kickDamage;
        
        private CharacterMovement _movement;
        
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();
        
        [Inject]
        private WeaponOnGroundFactory WeaponOnGroundFactory { get; set; }

        private void Awake()
        {
            EquipStartingWeapon();
        }

        public void Punch()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return;
            victim.Life.TakeDamage(_punchDamage);
        }

        public void Kick()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return;
            victim.Life.TakeDamage(_kickDamage);
        }

        public void UseWeapon()
        {
            if (CurrentWeaponType == null)
                return;
            if ( ! TryGetVictim(CurrentWeaponType.MaxRange, out CharacterMarker victim))
                return;
            CurrentWeaponType.Attack(GetComponent<CharacterMarker>(), victim);
        }

        public void SwapWeapon()
        {
            WeaponType currentWeaponType = CurrentWeaponType;
            WeaponOnGround pickedUpWeapon = Movement.CurrentSpot.GetObject<WeaponOnGround>();
            WeaponType pickedUpWeaponType = pickedUpWeapon?.Type;

            if (pickedUpWeapon != null)
            {
                pickedUpWeapon.PickUp();
            }

            if (currentWeaponType != null)
            {
                WeaponOnGroundFactory.SpawnWeapon(currentWeaponType, Movement.CurrentSpot.IndexOnMap);
            }

            _currentWeaponType = pickedUpWeaponType;
        }

        public void TryDisarm()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return;
            _currentWeaponType = victim.Attack.CurrentWeaponType;
            victim.Attack._currentWeaponType = null;
        }

        private bool TryGetVictim(int maxRange, out CharacterMarker victim)
        {
            victim = null;
            
            for (int i = 1; i <= maxRange; i++)
            {
                Spot adjacentSpot = Movement.CurrentSpot.GetAdjacentSpot(Movement.Direction * i);
                if (adjacentSpot == null)
                    continue;
            
                victim = adjacentSpot.GetObject<CharacterMovement>()?.GetComponent<CharacterMarker>();
                
                if (victim != null)
                    break;
            }
            
            return victim != null;
        }

        private void EquipStartingWeapon()
        {
            _currentWeaponType = _startingWeaponType;
        }

        public void Restore() => EquipStartingWeapon();
    }
}