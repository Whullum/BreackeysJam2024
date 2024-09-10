using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMarker : MonoBehaviour
    {
        private CharacterMovement _movement;
        private CharacterLife _life;
        private CharacterStrikes _strikes;
        
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();
        public CharacterLife Life => _life ??= GetComponent<CharacterLife>(); 
        public CharacterStrikes Strikes => _strikes ??= GetComponent<CharacterStrikes>(); 
    }
}