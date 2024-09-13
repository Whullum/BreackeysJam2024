using _Scripts.Gameplay.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Gameplay.Moves
{
    public class MoveOnTimeline : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private float _animTime = 0.2f;

        private Move _move;

        public Move Move => _move;

        [Inject]
        private Timeline _timeline;
        [Inject]
        private SoundManager _soundManager;
        private HorizontalLayoutGroup _layoutGroup;

        private bool _animating = false;
        private bool _destroyed = false;

        public void Init(Move move)
        {
            _layoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
            _move = move;
            _image.sprite = _move.Icon;
        }

        public void RemoveThis()
        {
            if (_destroyed)
                return;
            _timeline.RemoveMove(this);
        }

        public void Destroy()
        {
            if (_destroyed)
                return;

            _soundManager.PlayButtonClickSFX(ButtonClickSFX.CardRemove);
            _destroyed = true;
            _animating = true;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.one * 1.05f, _animTime));
            sequence.Append(transform.DOScale(Vector3.zero, _animTime));
            sequence.onComplete += () => Destroy(gameObject);
        }

        private void Start()
        {
            if (_move == null)
            {
                Destroy(gameObject);
                return;
            }

            DoAnimation();
        }
        
        private void Update()
        {
            if (_animating)
            {
                _layoutGroup.CalculateLayoutInputHorizontal();
                _layoutGroup.SetLayoutHorizontal();
            }
        }

        private void DoAnimation()
        {
            _animating = true;
            transform.localScale = Vector3.zero;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.one * 1f, _animTime));
            sequence.Append(transform.DOPunchScale(Vector3.one * 0.05f, _animTime, 2));
            sequence.onComplete += () => _animating = false;
        }
    }
}