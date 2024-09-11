using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Disarm")]
    public class DisarmMove : Move
    {
        public override void Execute(PlayerMarker player, out bool combo)
        {
            combo = false;
            if (player.Movement.IsInAir)
            {
                player.Movement.TryDescend();
                return;
            }

            combo = player.Attack.TryDisarm();
        }
    }
}