using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.SpotSystem
{
    /// <summary>
    /// We have 7 positions in an X axis (-3, -2, -1, 0 (initial position), 1, 2, 3).
    /// We can have one object or person (enemy or player) in each position.
    /// </summary>
    public class Spot : MonoBehaviour
    {
        private CharacterMovement _currentCharacter;
        private SpotMap _map;
    
        public bool IsOccupied => _currentCharacter != null;
        
        public void Init(SpotMap map)
        {
            _map = map;
        }
        
        public bool TryOccupy(CharacterMovement characterMovement)
        {
            if (IsOccupied)
            {
                Debug.LogError("Spot is already occupied");
                return false;
            }

            _currentCharacter = characterMovement;
            return true;
        }
        
        public void Leave()
        {
            _currentCharacter = null;
        }
    
    }
}
