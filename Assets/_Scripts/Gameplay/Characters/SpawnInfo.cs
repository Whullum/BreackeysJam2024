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
        private Vector2Int _spot;
        
        public GameObject Prefab => _prefab;
        
        public Vector2Int Spot => _spot;
    }
}