using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Turn around")]
    public class TurnAroundMove : Move
    {
        public override void Execute(PlayerMarker player, out bool combo)
        {
            combo = false;
            player.Movement.TryDescend();
            player.Movement.TurnAround();
        }
    }
}