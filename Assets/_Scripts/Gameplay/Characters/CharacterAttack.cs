using _Scripts.Gameplay.SpotSystem;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterAttack : MonoBehaviour, IRestorable
    {
        [SerializeField]
        private Weapon.Weapon _startingWeapon;
        
        private Weapon.Weapon _currentWeapon;

        public Weapon.Weapon CurrentWeapon => _currentWeapon;
        
        [SerializeField]
        private int _punchDamage;
        
        [SerializeField]
        private int _kickDamage;
        
        private CharacterMovement _movement;
        
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();

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
            if (CurrentWeapon == null)
                return;
            if ( ! TryGetVictim(CurrentWeapon.MaxRange, out CharacterMarker victim))
                return;
            CurrentWeapon.Attack(GetComponent<CharacterMarker>(), victim);
        }

        public void TryDisarm()
        {
            if ( ! TryGetVictim(1, out CharacterMarker victim))
                return;
            _currentWeapon = victim.Attack.CurrentWeapon;
            victim.Attack._currentWeapon = null;
        }

        private bool TryGetVictim(int maxRange, out CharacterMarker victim)
        {
            victim = null;
            
            for (int i = 1; i <= maxRange; i++)
            {
                Spot adjacentSpot = Movement.CurrentSpot.GetAdjacentSpot(Movement.Direction * i);
                if (adjacentSpot == null)
                    continue;
            
                victim = adjacentSpot.CurrentCharacter?.GetComponent<CharacterMarker>();
                
                if (victim != null)
                    break;
            }
            
            return victim != null;
        }

        private void EquipStartingWeapon()
        {
            _currentWeapon = _startingWeapon;
        }

        public void Restore() => EquipStartingWeapon();
    }
}