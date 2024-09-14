using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    [SerializeField] Color _highlightColor;
    [SerializeField] Color _originalColor;
    private Outline _outline;

    private void Awake() 
    {
        _outline = GetComponent<Outline>();
    }
    
    public void OnPointerEnter(PointerEventData _)
    {
        _outline.effectColor = _highlightColor;
        
    }

    public void OnPointerExit(PointerEventData _)
    {
        _outline.effectColor = _originalColor;
    }
}
