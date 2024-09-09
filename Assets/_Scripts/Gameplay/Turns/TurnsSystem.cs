using System;
using System.Collections.Generic;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Moves;
using _Scripts.Gameplay.UI;
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

        public void BuildTurnsSequence()
        {
            _turns = new List<Action>();
            for (var i = 0; i < Timeline.Moves.Length; i++)
            {
                _turns.Add(Timeline.Moves[i].Move.Execute);
                foreach (EnemyMarker enemy in EnemyContainer.Enemies)
                {
                    _turns.Add(enemy.Behaviour.GetActionForTunr(i));
                }
            }

            ExecuteSequence();
        }

        public void ExecuteSequence()
        {
            foreach (Action turn in _turns)
            {
                turn.Invoke();
            }
        }
    }
}