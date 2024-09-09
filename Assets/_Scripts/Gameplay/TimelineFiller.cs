using System;
using _Scripts.Gameplay.Moves;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class TimelineFiller : MonoBehaviour
    {
        [SerializeField]
        private Move[] _moves;
        
        [Inject]
        private Timeline Timeline { get; set; }

        private void Start()
        {
            foreach (Move move in _moves)
            {
                Timeline.AddMove(move);
            }
        }
    }
}