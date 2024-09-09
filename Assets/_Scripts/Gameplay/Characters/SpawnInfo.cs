using System;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    [Serializable]
    public struct SpawnInfo
    {
        [SerializeField]
        private GameObject _prefab;
        
        [SerializeField]
        private int _spot;
        
        public GameObject Prefab => _prefab;
        
        public int Spot => _spot;
    }
}