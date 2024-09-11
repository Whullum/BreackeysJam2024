using System.Collections.Generic;
using _Scripts.Gameplay.Moves;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class ActionBar : MonoBehaviour
    {
        [SerializeField]
        private List<Move> _possibleMoves;

        [SerializeField]
        private GameObject _movePrefab;

        [SerializeField]
        private Transform _actionCardParent;
        
        [Inject]
        private ContainerFactory ContainerFactory { get; set; }
    
        private void Start()
        {
            SpawnMoves();
        }


        private void SpawnMoves()
        {
            foreach (var move in _possibleMoves)
            {
                MoveInActionBar newMove = ContainerFactory.Instantiate<MoveInActionBar>(_movePrefab, _actionCardParent);
                newMove.Init(move);
            }
        }
    }
}
