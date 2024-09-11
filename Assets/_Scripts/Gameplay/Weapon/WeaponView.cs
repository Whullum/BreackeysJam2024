using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private CharacterAttack _attack;
        
        private void Update()
        {
            _spriteRenderer.sprite = _attack.CurrentWeaponType?.Sprite;
        }
    }
}