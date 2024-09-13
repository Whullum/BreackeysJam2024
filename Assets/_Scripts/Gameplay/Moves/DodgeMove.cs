using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Dodge")]
    public class DodgeMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            if (player.Movement.IsInAir)
            {
                player.Movement.TryDescend();
                return;
            }
            player.Life.Dodge();
        }
    }
}