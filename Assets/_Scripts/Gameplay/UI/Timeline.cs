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
        
        [SerializeField]
        private Transform _moveParent;

        private List<MoveOnTimeline> _moves = new List<MoveOnTimeline>();

        public MoveOnTimeline[] Moves => _moves.ToArray();

        [Inject]
        private ContainerFactory _containerFactory;

        private bool _isInFightStage = false;

        public void RemoveMove(MoveOnTimeline move)
        {
            if (_isInFightStage)
            {
                return;
            }
            _moves.TryRemove(move);
            if (move)
            {
                move.Destroy();
            }
        }
        
        public void AddMove(Move move)
        {
            if (_isInFightStage)
            {
                return;
            }
            MoveOnTimeline newMove = _containerFactory.Instantiate<MoveOnTimeline>(_movePrefab, _moveParent.position, _moveParent);
            newMove.Init(move);
            _moves.Add(newMove);    
        }

        public void FightStageStarted()
        {
            _isInFightStage = true;
        }

        public void ClearTimeline()
        {
            if (_isInFightStage)
            {
                return;
            }
            for (int i = _moves.Count-1; i >= 0; i--)
            {
                MoveOnTimeline move = _moves[i];
                RemoveMove(move);
            }
        }
    }
}