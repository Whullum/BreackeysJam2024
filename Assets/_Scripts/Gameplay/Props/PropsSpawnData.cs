using System;
using _Scripts.Gameplay.SpotSystem;
using UnityEngine;

namespace _Scripts.Gameplay.Props
{
    [Serializable]
    public struct PropsSpawnData
    {
        [SerializeField]
        private SpotObject _prefab;
        
        [SerializeField]
        private int _spot;

        public GameObject Prefab => _prefab.gameObject;
        
        public int Spot => _spot;
    }
}