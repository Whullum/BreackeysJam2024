using System;
using _Scripts.Gameplay.Weapon;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    [Serializable]
    public struct EnemySpawnInfo
    {
        [SerializeField]
        private int _spot;
        
        [SerializeField]
        private EnemyIntention[] _behaviourOverride;
        
        [SerializeField]
        private WeaponType _weaponOverride; 
        
        public int Spot => _spot;

        public EnemyIntention[] BehaviourOverride => _behaviourOverride;

        public WeaponType WeaponOverride => _weaponOverride;
    }
}