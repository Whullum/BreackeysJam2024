using System;
using UnityEngine;

namespace _Scripts.Gameplay.Props
{
    public class ElectricPoleView : MonoBehaviour
    {
	    [SerializeField]
        private ElectricPole _model;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private Sprite[]  _spritePerDurability;
        
        private void Update()
        {
            _spriteRenderer.sprite = _spritePerDurability[_model.Durability];
        }
    }
}