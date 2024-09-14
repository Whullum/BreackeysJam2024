using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.UI.Project
{
    public class ForesightUsage : MonoBehaviour
    {
        [SerializeField]
        private int _startingUses;

        public int UsesLeft { get; private set; }

        private void Awake()
        {
            ResetUses();
        }

        public void ResetUses()
        {
            UsesLeft = _startingUses;
        }

        public bool TryUse()
        {
            if (UsesLeft <= 0)
                return false;
            
            UsesLeft--;
            return true;
        }
    }
}