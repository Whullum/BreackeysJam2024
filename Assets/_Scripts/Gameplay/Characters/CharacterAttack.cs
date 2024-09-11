﻿using _Scripts.Gameplay.SpotSystem;
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

        public bool Punch()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return false;
            victim.Life.TakeDamage(_punchDamage);
            return true;
        }

        public bool Kick()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return false;
            Debug.Log("Has victim");

            victim.Life.TakeDamage(_kickDamage);
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