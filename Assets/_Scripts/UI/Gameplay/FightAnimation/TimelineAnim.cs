using System.Collections;
using System.Collections.Generic;
using _Scripts.Gameplay.UI;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class TimelineAnim : FightStageAnimator
{
    [SerializeField]
    private float _anchorYEndMin;
    
    [SerializeField]
    private float _anchorYEndMax;

    [SerializeField]
    private float _animLength;

    [Inject]
    private Timeline _timeline;

    public override void TransitionToFightStage()
    {
        _timeline.FightStageStarted();
        RectTransform rt = (RectTransform)transform;
        rt.DOAnchorMax(new Vector2(rt.anchorMax.x, _anchorYEndMax), _animLength);
        rt.DOAnchorMin(new Vector2(rt.anchorMin.x, _anchorYEndMin), _animLength);
    }

}
