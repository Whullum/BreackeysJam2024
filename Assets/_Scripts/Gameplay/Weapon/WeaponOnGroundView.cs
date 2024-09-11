using System;
using UnityEngine;

namespace _Scripts.Gameplay.Weapon
{
    public class WeaponOnGroundView : MonoBehaviour
    {
        [SerializeField]
        private WeaponOnGround _model; 
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void Update()
        {
            _spriteRenderer.sprite = _model.Type?.Sprite;
        }
    }
}