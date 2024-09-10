using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Chains")]
    public class ChainsWeapon : Weapon
    {
        public override void Attack(CharacterMarker attacker, CharacterMarker victim)
        {
            victim.Life.TakeDamage(Damage);

            int pullSpot = attacker.Movement.CurrentSpot.IndexOnMap + attacker.Movement.Direction;
            victim.Movement.GoToSpot(pullSpot);
        }
    }
}