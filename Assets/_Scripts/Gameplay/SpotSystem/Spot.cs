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
        private SpotMap _map;
        
        public CharacterMovement CurrentCharacter { get; private set; }

        public int IndexOnMap { get; private set; }

        public bool IsOccupied => CurrentCharacter != null;
        
        public void Init(SpotMap map, int indexOnMap)
        {
            _map = map;
            IndexOnMap = indexOnMap;
        }

        public Spot GetAdjacentSpot(int delta) => _map.GetSpot(IndexOnMap + delta);

        public bool TryOccupy(CharacterMovement characterMovement)
        {
            if (IsOccupied)
            {
                Debug.LogError("Spot is already occupied");
                return false;
            }

            CurrentCharacter = characterMovement;
            return true;
        }
        
        public void Leave()
        {
            CurrentCharacter = null;
        }
    
    }
}
