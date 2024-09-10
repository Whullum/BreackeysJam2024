using _Scripts.Gameplay.SpotSystem;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterStrikes : MonoBehaviour
    {
        [SerializeField]
        private int _punchDamage;
        
        [SerializeField]
        private int _kickDamage;
        
        private CharacterMovement _movement;
        
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();

        public void Punch()
        {
            if ( ! TryGetVictim(out CharacterMarker victim))
                return;
            victim.Life.TakeDamage(_punchDamage);
        }

        public void Kick()
        {
            if ( ! TryGetVictim(out CharacterMarker victim))
                return;
            victim.Life.TakeDamage(_kickDamage);
        }

        private bool TryGetVictim(out CharacterMarker victim)
        {
            Spot adjacentSpot = Movement.CurrentSpot.GetAdjacentSpot(Movement.Direction);

            
            if (adjacentSpot == null)
            {
                victim = null;
                return false;
            }
            
            victim = adjacentSpot.CurrentCharacter?.GetComponent<CharacterMarker>();
            return victim != null;
        }
    }
}