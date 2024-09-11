using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Use weapon")]
    public class UseWeaponMove : Move
    {
        public override void Execute(PlayerMarker player, out bool combo)
        {
            combo = false;
            player.Attack.UseWeapon();
        }
    }
}