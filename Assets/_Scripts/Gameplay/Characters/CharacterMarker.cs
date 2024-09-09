using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMarker : MonoBehaviour
    {
        private CharacterMovement _movement;
        
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>(); 
    }
}