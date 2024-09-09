using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Gameplay.Moves
{
    public class MoveOnTimeline : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        private Move _move;

        public Move Move => _move;

        public void Init(Move move)
        {
            _move = move;
            _image.sprite = _move.Icon;
        }

        private void Start()
        {
            if (_move == null)
            {
                Destroy(gameObject);
            }
        }
    }
}