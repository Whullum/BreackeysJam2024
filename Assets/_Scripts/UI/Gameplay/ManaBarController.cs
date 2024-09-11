using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public void SetManaBar(float percentage)
    {
        _image.fillAmount = Mathf.Clamp01(percentage);
    }

    public void SetManaBar(int currMana, int maxMana)
    {
        _image.fillAmount = Mathf.Clamp01(((float)currMana)/maxMana);
    }

    [Button]
    public void TestManaBar()
    {
        SetManaBar(3,12);
    }

    [Button]
    public void TestManaBar2()
    {
        SetManaBar(0,12);
    }

    [Button]
    public void TestManaBar3()
    {
        SetManaBar(12,12);
    }

    [Button]
    public void TestManaBar4()
    {
        SetManaBar(12);
    }

    [Button]
    public void TestManaBar5()
    {
        SetManaBar(0.12f);
    }
}
