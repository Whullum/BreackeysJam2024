using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _manaText;

    public void SetManaBar(int currMana)
    {
        _manaText.text = currMana.ToString();
    }

}
