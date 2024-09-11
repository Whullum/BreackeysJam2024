using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class FightStageAnimator : MonoBehaviour
{
    public abstract void TransitionToFightStage();
}

public class FightStageController : MonoBehaviour
{   
    [SerializeField]
    private List<FightStageAnimator> _animations;

    [Button]
    public void SwitchToFightStage()
    {
        foreach (var anim in _animations)
        {
            anim.TransitionToFightStage();
        }
    }
}
