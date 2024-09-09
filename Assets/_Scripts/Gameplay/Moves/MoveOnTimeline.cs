using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    public class MoveOnTimeline : MonoBehaviour
    {
        private Move _move;

        public void Init(Move move)
        {
            _move = move;
        }
    }
}