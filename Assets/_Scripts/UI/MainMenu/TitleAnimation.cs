using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> _titles;

    [Inject]
    private SoundManager _soundManager;

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
            Debug.Log(title);
            yield return new WaitForSeconds(0.5f);
            Debug.Log(title);
        }
    }


}
