using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackgroundSelector : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _sprites;

    [SerializeField]
    private Image _image;

    private void Awake()
    {
        _image.sprite = _sprites[Random.Range(0, _sprites.Count)];
    }
}
