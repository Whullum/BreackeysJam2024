using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RectTransform _toScale;

    [SerializeField]
    private float _scalingFactor = 1.2f;

    [SerializeField]
    private float _animTime = 0.2f;

    [Button]
    private void SetParentAsRect()
    {
        if (!_toScale)
        {
            _toScale = GetComponent<RectTransform>();
        }
    }

    public void OnPointerEnter(PointerEventData _)
    {
        _toScale.DOBlendableScaleBy(Vector3.one * (_scalingFactor - 1f), _animTime);
    }

    public void OnPointerExit(PointerEventData _)
    {
        _toScale.DOBlendableScaleBy(Vector3.one * (1f-_scalingFactor), _animTime);
    }
}
