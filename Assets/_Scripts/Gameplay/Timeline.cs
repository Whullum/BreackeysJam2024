using System.Collections.Generic;
using _Scripts.Gameplay.Moves;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class Timeline : MonoBehaviour
    {
        [SerializeField]
        private GameObject _movePrefab;
        
        private readonly List<MoveOnTimeline> _moves = new List<MoveOnTimeline>();
        
        [Inject]
        private ContainerFactory ContainerFactory { get; set; }
        
        public void AddMove(Move move)
        {
            MoveOnTimeline newMove = ContainerFactory.Instantiate<MoveOnTimeline>(_movePrefab, transform.position, transform);
            newMove.Init(move);
            _moves.Add(newMove);
        }
    }
}