using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.Extentions;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Moves;
using _Scripts.Gameplay.UI;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Turns
{
    public class FightPlayer : MonoBehaviour
    {
        [Inject]
        private Timeline Timeline { get; set; }
        
        [Inject]
        private EnemyContainer EnemyContainer { get; set; }
        
        [Inject]
        private PlayerMarker Player { get; set; }
        
        [Inject]
        private ComboSystem ComboSystem { get; set; }

        [Inject]
        private LevelLoader _levelLoader;
        
        [Inject]
        private WeaponOnGroundFactory WeaponOnGroundFactory { get; set; }

        private bool IsWinConditionMet => EnemyContainer.Enemies.All(e => e.Life.IsDead);
        
        private bool IsLoseConditionMet => Player.Life.IsDead;
        
        public event Action TurnStarted;

        public void PlayFight()
        {   
            StartCoroutine(Fight());
        }

        private IEnumerator Fight()
        {
            // Fight is limited by 99 turns. This is a sanity check.
            for (int turn = 0; turn < 99; turn++)
            {
                TurnStarted?.Invoke();
                ComboSystem.SwitchTurns();
                
                if (turn < Timeline.Moves.Length)
                {
                    MoveOnTimeline moveOnTimeline = Timeline.Moves[turn];
                    moveOnTimeline.Move.Execute(Player);
                }
                else
                {
                    Player.Movement.TryDescend();
                }
                
                yield return new WaitForSeconds(0.3f);
                if (ComboSystem.IsComboActive)
                {
                    Debug.Log("COMBO!");
                    continue;
                }

                foreach (EnemyMarker enemy in EnemyContainer.Enemies)
                {
                    enemy.Behaviour.PerformNextTurn();
                yield return new WaitForSeconds(0.3f);
                }


                if (IsWinConditionMet || IsLoseConditionMet)
                    break;
            }

            Debug.Log("Fight is over");
            
            if (IsWinConditionMet)
            {
                _levelLoader.LoadNextLevel();
            }
            else
            {
                _levelLoader.ReloadLevel();
            }

            //RestoreScene();
        }

        private void RestoreScene()
        {
            Player.DiscardMemebers();
            EnemyContainer.Enemies.Foreach(e => e.DiscardMemebers());
            
            Player.RestoreMemebers();
            EnemyContainer.Enemies.Foreach(e => e.RestoreMemebers());
            
            WeaponOnGroundFactory.Discard();
        }
    }
}