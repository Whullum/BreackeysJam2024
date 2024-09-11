using System;
using _Scripts.Gameplay.Turns;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterLife : MonoBehaviour, IDiscardable, IRestorable
    {
        [SerializeField]
        private int _startingHealth;
        
        private int _health;
        
        public bool IsDead => _health <= 0;

        public bool IsDodging { get; private set; }
        
        public event Action Died;
        
        [Inject]
        private FightPlayer FightPlayer { get; set; }

        private void Awake()
        {
            _health = _startingHealth;
            FightPlayer.TurnStarted += StopDodge;
        }

        public void TakeDamage(int damage)
        {
            if (IsDodging || damage <= 0 || IsDead)
                return;

            Debug.Log(_health);
            _health -= damage;
            
            if (_health <= 0)
                Kill();
        }

        public void Dodge()
        {
            IsDodging = true;
        }

        public void StopDodge()
        {
            IsDodging = false;
        }
        
        [Button]
        public void Kill()
        {
            if ( ! Application.isPlaying)
                throw new InvalidOperationException("No killing in edit mode!");
            
            _health = 0;
            Died?.Invoke();
            Debug.Log($"Add death animation here");
            gameObject.SetActive(false);
        }

        public void Discard() => _health = _startingHealth;

        public void Restore() => gameObject.SetActive(true);
    }
}