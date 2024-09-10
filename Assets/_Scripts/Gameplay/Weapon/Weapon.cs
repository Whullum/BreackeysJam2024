using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Weapon
{
    public abstract class Weapon : ScriptableObject
    {
        [SerializeField]
        private int _maxRange;

        [SerializeField]
        private int _damage;
        
        [SerializeField]
        private Sprite _sprite;
        
        [SerializeField]
        private int _animationSet;

        public int MaxRange => _maxRange;
        public int Damage => _damage;
        public Sprite Sprite => _sprite;
        public int AnimationSet => _animationSet;

        public abstract void Attack(CharacterMarker attacker, CharacterMarker victim);
    }
}