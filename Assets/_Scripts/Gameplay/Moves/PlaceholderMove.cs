using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/PlaceholderMove")]
    public class PlaceholderMove : Move
    {
        public override void Execute(PlayerMarker _)
        {
            Debug.Log($"{Name} move executed");
        }
    }
}