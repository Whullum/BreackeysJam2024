using _Scripts.Gameplay.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Gameplay.Moves
{
    public class MoveInActionBar : MonoBehaviour
    {
        [SerializeField]
        private Move _move;
        
        [SerializeField]
        private Image _icon;
        
        [SerializeField]
        private TMP_Text _label;


        [SerializeField]
        private TMP_Text _description;
        
        [Inject]
        private Timeline Timeline { get; set; }

        public void Init(Move move)
        {
            _move = move;
            if (_icon != null & _icon)
            {
                _icon.sprite = _move.Icon;
            }
            if (_label != null & _label)
            {
                _label.text = _move.name;
            }
            //if (_description != null & _description)
            //{
            //    _description.text = _move.Description;
            //}
        }

        private void Start()
        {
            if (_move == null)
            {
                Destroy(gameObject);
            }
        }

        public void AddThisToTimeline()
        {
            Timeline.AddMove(_move);
        }



        private void OnValidate()
        {
            if (_move == null)
                return;
            
            _label.text = _move.Name;
            _icon.sprite = _move.Icon;
        }
    }
}