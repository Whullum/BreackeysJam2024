using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Swap weapon")]
    public class SwapWeaponMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            if (player.Movement.IsInAir)
            {
                player.Movement.TryDescend();
                return;
            }
            player.Attack.SwapWeapon();
        }
    }
}