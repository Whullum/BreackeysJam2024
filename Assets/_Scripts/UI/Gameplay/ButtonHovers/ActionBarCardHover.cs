using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarCardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float _offset = 50;
    [SerializeField]
    [Range(1f, 5f)]
    private float _scalingFactor = 2f;

    [SerializeField]
    private float _animDuration = 0.2f;

    [SerializeField]
    private RectTransform _description;

    [SerializeField]
    private float _descriptionAnimTime = 0.4f;

    [SerializeField]
    private float _descriptionAnimDelay = 2f;

    private float _startPosY = 0f;
    private float _endPosY => _startPosY + _offset;

    private float _startScale = 1f;
    private float _endScale => 1f * _scalingFactor;

    private bool _isHovering = false;

    private RectTransform rt => (RectTransform)transform;

    private Coroutine _descShowCoroutine;

    private void Awake()
    {
        _startPosY = rt.anchoredPosition.y;
        _startScale = rt.localScale.x;
        _description.localScale = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData _)
    {
        _isHovering = true;
        Vector2 targetPos = new Vector2(0f, _endPosY);
        Vector3 targetScale = Vector3.one * _endScale;
        rt.DOAnchorPos(targetPos, _animDuration);
        rt.DOScale(targetScale, _animDuration);
        _descShowCoroutine = StartCoroutine(TryShowDescription());
    }



    public void OnPointerExit(PointerEventData _)
    {
        _isHovering = false;
        StopCoroutine(_descShowCoroutine);
        if (!Mathf.Approximately(Vector3.SqrMagnitude(_description.localScale),0f))
        {
            _description.DOKill();
            _description.DOScale(0f, _animDuration);
        }
        Vector2 targetPos = new Vector2(0f, _startPosY);
        Vector3 targetScale = Vector3.one * _startScale;
        rt.DOAnchorPos(targetPos, _animDuration);
        rt.DOScale(targetScale, _animDuration);
    }   

    private IEnumerator TryShowDescription()
    {
        yield return new WaitForSeconds(_descriptionAnimDelay);
        var a = _description.DOScale(Vector3.one * 1f, _descriptionAnimTime);
        a.onComplete += () => _description.DOPunchScale(Vector3.one * 0.05f, _descriptionAnimTime, 2);
    }

}
