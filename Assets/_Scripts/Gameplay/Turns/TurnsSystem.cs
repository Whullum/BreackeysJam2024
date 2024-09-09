using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Moves;
using _Scripts.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Turns
{
    public class TurnsSystem
    {
        private List<Action> _turns = new List<Action>();
        
        [Inject]
        private Timeline Timeline { get; set; }
        
        [Inject]
        private EnemyContainer EnemyContainer { get; set; }
        
        [Inject]
        private PlayerMarker Player { get; set; }

        public void BuildTurnsSequence()
        {
            _turns = new List<Action>();
            for (var i = 0; i < Timeline.Moves.Length; i++)
            {
                MoveOnTimeline moveOnTimeline = Timeline.Moves[i];
                _turns.Add(() => moveOnTimeline.Move.Execute(Player));
                foreach (EnemyMarker enemy in EnemyContainer.Enemies)
                {
                    _turns.Add(enemy.Behaviour.GetActionForTunr(i));
                }
            }

            ExecuteSequence();
        }

        public async void ExecuteSequence()
        {
            foreach (Action turn in _turns)
            {
                turn.Invoke();
                await Task.Delay(500);
            }
        }
    }
}