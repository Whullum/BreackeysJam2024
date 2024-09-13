using System;
using _Scripts.Gameplay.Execution;
using DG.Tweening;
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

        [ShowNativeProperty]
        public bool IsDodging { get; private set; }
        
        public event Action Died;
        public event Action Dodged;
        
        [Inject]
        private FightPlayer _fightPlayer;

        private void Awake()
        {
            _health = _startingHealth;
            _fightPlayer.TurnStarted += StopDodge;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"{damage} {IsDead} {IsDodging}");
            if (damage <= 0 || IsDead)
                return;

            if (IsDodging)
            {
                Dodged?.Invoke();
                return;
            }
            
            _health -= damage;
            transform.DOKill();
            transform.DOShakeScale(0.2f, Vector3.one * 0.5f);
            
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