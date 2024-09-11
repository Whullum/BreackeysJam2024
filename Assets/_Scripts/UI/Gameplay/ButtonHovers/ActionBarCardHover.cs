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

    private float _startPosY = 0f;
    private float _endPosY => _startPosY + _offset;

    private float _startScale = 1f;
    private float _endScale => 1f * _scalingFactor;

    private RectTransform rt => (RectTransform)transform;

    private void Awake()
    {
        _startPosY = rt.anchoredPosition.y;
        _startScale = rt.localScale.x;
    }

    public void OnPointerEnter(PointerEventData _)
    {
        Vector2 targetPos = new Vector2(0f, _endPosY);
        Vector3 targetScale = Vector3.one * _endScale;
        rt.DOAnchorPos(targetPos, _animDuration);
        rt.DOScale(targetScale, _animDuration);
    }

    public void OnPointerExit(PointerEventData _)
    {
        Vector2 targetPos = new Vector2(0f, _startPosY);
        Vector3 targetScale = Vector3.one * _startScale;
        rt.DOAnchorPos(targetPos, _animDuration);
        rt.DOScale(targetScale, _animDuration);
    }

}
