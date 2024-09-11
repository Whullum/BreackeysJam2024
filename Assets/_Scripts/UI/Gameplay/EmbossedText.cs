using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class EmbossedText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _sourceText;
    private TMP_Text _targetText;

    private void Awake()
    {
        _targetText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (_targetText == null || !_targetText)
            return;
        _targetText.text = _sourceText.text;
    }
}
