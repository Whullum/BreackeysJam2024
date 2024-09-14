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

        public bool IsDead { get; private set; }

        public bool IsDodging { get; private set; }

        public bool IsGuarding { get; private set; }
        
        public event Action Died;
        public event Action TookHit;
        public event Action Dodged;
        public event Action Guarded;
        public event Action Blocked;
        public event Action StoppedGuard;
        
        [Inject]
        private FightPlayer _fightPlayer;
        
        [Inject]
        private ComboSystem _comboSystem;

        private void Awake()
        {
            _health = _startingHealth;
            _fightPlayer.TurnStarted += StopDodge;
            _fightPlayer.TurnStarted += StopGuard;
            _comboSystem.ComboEnded += CheckDeath;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            if (IsGuarding)
            {
                Blocked?.Invoke();
                return;
            }
            if (IsDodging)
            {
                Dodged?.Invoke();
                return;
            }

            damage = Math.Max(0, damage);
            _health -= damage;
            TookHit?.Invoke();

            if ( ! _comboSystem.IsComboActive)
                CheckDeath();
        }

        private void CheckDeath()
        {
            if (_health <= 0)
                Kill();
        }

        public void Guard()
        {
            IsGuarding = true;
            Guarded?.Invoke();
        }

        public void StopGuard()
        {
            IsGuarding = false;
            StoppedGuard?.Invoke();
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
            IsDead = true;
            Died?.Invoke();
            Debug.Log($"Add death animation here");
            gameObject.SetActive(false);
        }

        public void Discard()
        {
            _health = _startingHealth;
            IsDead = false;
            StopGuard();
            StopDodge();
        }

        public void Restore() => gameObject.SetActive(true);
    }
}