using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStageButtonAnim : FightStageAnimator
{
    [SerializeField]
    private GameObject _buttons;

    public override void TransitionToFightStage()
    {
        _buttons.SetActive(false);
    }
}
