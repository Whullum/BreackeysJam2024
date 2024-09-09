using _Scripts.Gameplay.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Gameplay.Moves
{
    public class MoveOnTimeline : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        private Move _move;

        public Move Move => _move;
        
        [Inject]
        private Timeline Timeline { get; set; }

        public void Init(Move move)
        {
            _move = move;
            _image.sprite = _move.Icon;
        }

        public void RemoveThis()
        {
            Timeline.RemoveMove(this);
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