using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Walk move")]
    public class WalkMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            player.Movement.TryGoForward();
        }
    }
}