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
        
        public event Action Died;
        public event Action Dodged;
        
        [Inject]
        private FightPlayer _fightPlayer;
        
        [Inject]
        private ComboSystem _comboSystem;

        private void Awake()
        {
            _health = _startingHealth;
            _fightPlayer.TurnStarted += StopDodge;
            _comboSystem.ComboEnded += CheckDeath;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            if (IsDodging)
            {
                Dodged?.Invoke();
                return;
            }

            damage = Math.Max(0, damage);
            _health -= damage;
            transform.DOKill();
            transform.DOShakeScale(0.2f, Vector3.one * 0.5f);

            if ( ! _comboSystem.IsComboActive)
                CheckDeath();
        }

        private void CheckDeath()
        {
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
            IsDead = true;
            Died?.Invoke();
            Debug.Log($"Add death animation here");
            gameObject.SetActive(false);
        }

        public void Discard()
        {
            _health = _startingHealth;
            IsDead = false;
        }

        public void Restore() => gameObject.SetActive(true);
    }
}