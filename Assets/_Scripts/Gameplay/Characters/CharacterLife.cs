using System;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterLife : MonoBehaviour
    {
        [SerializeField]
        private int _health;
        
        //Health
        public bool IsDead => _health <= 0;

        public event Action Died;

        public void TakeDamage(int damage)
        {
            if (damage <= 0 || IsDead)
                return;

            _health -= damage;
            
            if (_health <= 0)
                Kill();
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
    }
}