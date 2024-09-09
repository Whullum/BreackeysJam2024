using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Turn around move")]
    public class TurnAroundMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            player.Movement.TurnAround();
        }
    }
}