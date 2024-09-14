using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class TitleAnimation : MonoBehaviour
{
    [Inject]
    private SoundManager _soundManager;

    [SerializeField]
    private List<RectTransform> _titles;

    [Button]
    private void Start()
    {
        StartCoroutine(AnimTitle());
    }

    private IEnumerator AnimTitle()
    {
        foreach (var title in _titles)
        {
            title.localScale = Vector3.one * 30f;
        }
        foreach (var title in _titles)
        {
            var scale = title.DOScale(1f, 0.3f);
            scale.onComplete += () => _soundManager.CardPlayedSFX(CardPlayedSFX.Punch);
            yield return new WaitForSeconds(0.5f);
        }
    }


}
