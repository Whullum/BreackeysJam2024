using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Punch")]
    public class PunchMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            player.Strikes.Punch();
        }
    }
}