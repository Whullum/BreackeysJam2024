using System;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.Extentions;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Moves;
using _Scripts.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Turns
{
    public class FightPlayer
    {
        [Inject]
        private Timeline Timeline { get; set; }
        
        [Inject]
        private EnemyContainer EnemyContainer { get; set; }
        
        [Inject]
        private PlayerMarker Player { get; set; }
        
        [Inject]
        private ComboSystem ComboSystem { get; set; }

        private bool IsWinConditionMet => EnemyContainer.Enemies.All(e => e.Life.IsDead);
        
        private bool IsLoseConditionMet => Player.Life.IsDead;
        
        public event Action TurnStarted;

        public async void PlayFight()
        {
            // For now fight is limited by 20 turns. Later fight must only be limited by win/lose conditions
            for (int turn = 0; turn < 20; turn++)
            {
                TurnStarted?.Invoke();
                
                if (turn < Timeline.Moves.Length)
                {
                    MoveOnTimeline moveOnTimeline = Timeline.Moves[turn];
                    moveOnTimeline.Move.Execute(Player);
                }
                
                await Task.Delay(300);
                
                if (ComboSystem.TrySpendCombo())
                    continue;

                foreach (EnemyMarker enemy in EnemyContainer.Enemies)
                {
                    enemy.Behaviour.PerformTurn(turn);
                    await Task.Delay(300);
                }


                if (IsWinConditionMet || IsLoseConditionMet)
                    break;
            }

            Debug.Log("Fight is over");
            
            RestoreScene();
        }

        private void RestoreScene()
        {
            Player.DiscardMemebers();
            EnemyContainer.Enemies.Foreach(e => e.DiscardMemebers());
            
            Player.RestoreMemebers();
            EnemyContainer.Enemies.Foreach(e => e.RestoreMemebers());
        }
    }
}