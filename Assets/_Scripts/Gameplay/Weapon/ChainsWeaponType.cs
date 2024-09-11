using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Chains")]
    public class ChainsWeaponType : WeaponType
    {
        public override void Attack(CharacterMarker attacker, CharacterMarker victim)
        {
            victim.Life.TakeDamage(Damage);

            Vector2Int pullSpot = attacker.Movement.Coordinates + new Vector2Int(attacker.Movement.Direction, 0);
            victim.Movement.GoToSpot(pullSpot);
        }
    }
}