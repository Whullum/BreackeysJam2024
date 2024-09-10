using System.Collections.Generic;
using _Scripts.Extentions;
using _Scripts.Gameplay.Moves;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class Timeline : MonoBehaviour
    {
        [SerializeField]
        private GameObject _movePrefab;
        
        private List<MoveOnTimeline> _moves = new List<MoveOnTimeline>();

        public MoveOnTimeline[] Moves => _moves.ToArray();

        [Inject]
        private ContainerFactory ContainerFactory { get; set; }

        public void RemoveMove(MoveOnTimeline move)
        {
            _moves.TryRemove(move);
            Destroy(move.gameObject);
        }
        
        public void AddMove(Move move)
        {
            MoveOnTimeline newMove = ContainerFactory.Instantiate<MoveOnTimeline>(_movePrefab, transform.position, transform);
            newMove.Init(move);
            _moves.Add(newMove);    
        }

    }
}