using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterBlood : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;
        
        [SerializeField]
        private float _bloodDuration;

        [SerializeField]
        private CharacterLife _model;

        private void Awake()
        {
            _model.TookHit += DropBlood;
        }

        public void DropBlood()
        {
            _particleSystem.Play();
        }
    }
}