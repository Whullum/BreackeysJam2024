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
        
        [Inject]
        private Timeline Timeline { get; set; }

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