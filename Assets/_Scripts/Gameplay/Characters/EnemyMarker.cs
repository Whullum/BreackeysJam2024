using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class EnemyMarker : CharacterMarker
    {
        private EnemyBehaviour _behaviour;
        
        public EnemyBehaviour Behaviour => _behaviour ??= GetComponent<EnemyBehaviour>(); 
    }
}