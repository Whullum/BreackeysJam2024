using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Walk")]
    public class WalkMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            player.Movement.TryDescend();
            player.Movement.TryGoForward();
        }
    }
}