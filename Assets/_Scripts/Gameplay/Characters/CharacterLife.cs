using System;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterLife : MonoBehaviour
    {
        [SerializeField]
        private float _health;
        
        //Health
        public bool IsDead => _health <= 0;

        [Button]
        public void Kill()
        {
            if ( ! Application.isPlaying)
                throw new InvalidOperationException("No killing in edit mode!");
            _health = 0;
            Debug.Log($"Add death animation here");
            gameObject.SetActive(false);
        }
    }
}