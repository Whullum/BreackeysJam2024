using System;
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
        
        private CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();
        
        [Inject]
        private WeaponOnGroundFactory WeaponOnGroundFactory { get; set; }
        
        [Inject]
        private SpotMap SpotMap { get; set; }

        public event Action Punched;
        public event Action Kicked;
        public event Action UsedWeapon;

        private void Awake()
        {
            EquipStartingWeapon();
        }

        public bool Punch()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return false;
            victim.Life.TakeDamage(_punchDamage);
            Punched?.Invoke();
            if (Movement.IsInAir)
            {
                Spot destination = null;
                for (int i = 10; i >= 0; i--)
                {
                    destination = SpotMap.GetSpot(Movement.Coordinates + new Vector2Int(i * Movement.Direction, 0));
                    if (destination != null && ! destination.IsOccupiedBy<CharacterMovement>())
                        break;
                }
                if (destination != null)
                    victim.Movement.GoToSpot(destination.Coordinates);
            }
            return true;
        }

        public bool Kick()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return false;

            victim.Life.TakeDamage(_kickDamage);
            Kicked?.Invoke();
            if (victim.Life.IsDead)
                return true;
            
            if (Movement.IsInAir)
            {
                victim.Movement.TryDescend();
                Movement.TryDescend();
            }
            else
            {
                victim.Movement.TryAscend();
                Movement.TryAscend();
            }
            
            return true;
        }

        public bool UseWeapon()
        {
            if (CurrentWeaponType == null)
                return false;
            if ( ! TryGetVictim(CurrentWeaponType.MaxRange, out CharacterMarker victim))
                return false;
            CurrentWeaponType.Attack(GetComponent<CharacterMarker>(), victim);
            UsedWeapon?.Invoke();
            return true;
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
                WeaponOnGroundFactory.SpawnWeapon(currentWeaponType, Movement.CurrentSpot.Coordinates);
            }

            _currentWeaponType = pickedUpWeaponType;
        }

        public bool TryDisarm()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return false;
            if (victim.Attack.CurrentWeaponType == null)
                return false;
            _currentWeaponType = victim.Attack.CurrentWeaponType;
            victim.Attack._currentWeaponType = null;
            return true;
        }

        private bool TryGetVictim(int maxRange, out CharacterMarker victim)
        {
            victim = null;
            
            for (int i = 1; i <= maxRange; i++)
            {
                Spot adjacentSpot = Movement.CurrentSpot.GetAdjacentSpot(new Vector2Int(Movement.Direction * i, 0));
                if (adjacentSpot == null)
                    continue;
            
                victim = adjacentSpot.GetObject<CharacterMovement>()?.GetComponent<CharacterMarker>();
                if (victim?.GetType() == GetComponent<CharacterMarker>().GetType())
                    victim = null;
                
                if (victim != null)
                    break;
            }
            
            return victim != null;
        }

        public void AssignStartingWeaponType(WeaponType startingWeaponType)
        {
            _startingWeaponType = startingWeaponType;
            EquipStartingWeapon();
        }

        private void EquipStartingWeapon()
        {
            _currentWeaponType = _startingWeaponType;
        }

        public void Restore() => EquipStartingWeapon();
    }
}