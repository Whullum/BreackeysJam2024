using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FightStageButtonAnim : FightStageAnimator
{
    [SerializeField]
    private GameObject _buttons;

    [SerializeField]
    private GameObject _timelineReset;

    [SerializeField]
    private RectTransform _actionBar;

    public override void TransitionToFightStage()
    {
        _buttons.SetActive(false);
        _timelineReset.SetActive(false);
        _actionBar.DOAnchorPos(Vector2.down * 1000f, 1f);
    }
}
