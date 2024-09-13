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
        
        private void Update()
        {
            float alpha = _model.Durability switch
            {
                0 => 0.3f,
                1 => 0.8f,
                _ => 1
            };
            _spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }
}