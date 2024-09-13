using System;
using System.Collections;
using System.Linq;
using _Scripts.Extentions;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Moves;
using _Scripts.Gameplay.UI;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Execution
{
    public class FightPlayer : MonoBehaviour
    {
        [Inject]
        private Timeline _timeline;
        
        [Inject]
        private EnemyContainer _enemyContainer;
        
        [Inject]
        private PlayerMarker _player;
        
        [Inject]
        private ComboSystem _comboSystem;

        [Inject]
        private LevelLoader _levelLoader;
        
        [Inject]
        private WeaponOnGroundFactory _weaponOnGroundFactory;

        private bool IsWinConditionMet => _enemyContainer.Enemies.All(e => e.Life.IsDead);
        
        private bool IsLoseConditionMet => _player.Life.IsDead;
        
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
                _comboSystem.SwitchTurns();
                
                if (turn < _timeline.Moves.Length)
                {
                    MoveOnTimeline moveOnTimeline = _timeline.Moves[turn];
                    moveOnTimeline.Move.Execute(_player);
                }
                else
                {
                    _player.Movement.TryDescend();
                }
                
                yield return new WaitForSeconds(0.3f);
                if (_comboSystem.IsComboActive)
                {
                    Debug.Log("COMBO!");
                    continue;
                }

                foreach (EnemyMarker enemy in _enemyContainer.Enemies)
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
            _player.DiscardMemebers();
            _enemyContainer.Enemies.Foreach(e => e.DiscardMemebers());
            
            _player.RestoreMemebers();
            _enemyContainer.Enemies.Foreach(e => e.RestoreMemebers());
            
            _weaponOnGroundFactory.Discard();
        }
    }
}