using System;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterLifeMeter : MonoBehaviour
    {
        [SerializeField]
        private CharacterLife _model;

        [SerializeField]
        private GameObject[] _dots;

        private void Update()
        {
            for (int i = 0; i < _dots.Length; i++)
            {
                _dots[i].SetActive(i < _model.Health);
            }
        }
    }
}