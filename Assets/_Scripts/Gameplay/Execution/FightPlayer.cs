using System;
using System.Collections;
using System.Linq;
using _Scripts.Extentions;
using _Scripts.Gameplay.Characters;
using _Scripts.Gameplay.Moves;
using _Scripts.Gameplay.Props;
using _Scripts.Gameplay.UI;
using _Scripts.Gameplay.Weapon;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Execution
{
    public class FightPlayer : MonoBehaviour
    {
        [SerializeField]
        private float _turnsPerSecond;
        
        [SerializeField]
        private int _maxTurns;
        
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
        private SoundManager _soundManager;

        [Inject]
        private WeaponOnGroundFactory _weaponOnGroundFactory;

        [Inject]
        private GameOverScreen _gameOverScreen;

        [Inject]
        private VictoryScreen _victoryScreen;

        [Inject]
        private PropFactory _propFactory;
        
        private FightState _currentState;

        private float TurnsPeriod => 1 / _turnsPerSecond;
        
        private bool IsWinConditionMet => _enemyContainer.Enemies.All(e => e.Life.IsDead);

        private bool IsLoseConditionMet => _player.Life.IsDead;

        public bool IsPlanning => _currentState == FightState.Planning;
        
        public bool IsForeseeing => _currentState == FightState.Foresight;

        public bool IsFightingReal => _currentState == FightState.RealFight;

        public event Action TurnStarted;

        public void PlayFight(bool real)
        {
            if (IsPlanning)
            {
                StartCoroutine(Fight(real));
            }
        }

        public void StopFight()
        {
            if ( ! IsForeseeing)
                return;
            
            StopAllCoroutines();
            _currentState = FightState.Planning;
            Debug.Log("Fight is over");
            _soundManager.SwitchMusicState(GameplayAudioState.Calm);
            RestoreScene();
        }

        private IEnumerator Fight(bool real)
        {
            _currentState = real ? FightState.RealFight : FightState.Foresight;
            _soundManager.SwitchMusicState(GameplayAudioState.Storm);

            yield return new WaitForSeconds(2f); // wait for the good part of the music to catch up XD
            for (int turn = 0; turn < _maxTurns; turn++)
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

                yield return new WaitForSeconds(TurnsPeriod);

                _comboSystem.UpdateComboState();
                if (_comboSystem.IsComboActive)
                {
                    Debug.Log("COMBO!");
                    continue;
                }

                foreach (EnemyMarker enemy in _enemyContainer.Enemies)
                {
                    enemy.Behaviour.PerformNextTurn();
                    yield return new WaitForSeconds(TurnsPeriod);
                }


                if (IsWinConditionMet || IsLoseConditionMet)
                {
                    break;
                }
            }
            if (real)
            {
                if (IsWinConditionMet)
                {
                    _victoryScreen.ShowVictoryScreen();
                }
                else
                {
                    _gameOverScreen.ShowGameOver();
                }
                _soundManager.StopRepeatAudio();
            }
            else
            {
                StopFight();
            }
            
            _soundManager.SwitchMusicState(GameplayAudioState.Calm);
        }

        private void RestoreScene()
        {
            _player.DiscardMemebers();
            _enemyContainer.Enemies.Foreach(e => e.DiscardMemebers());

            _player.RestoreMemebers();
            _enemyContainer.Enemies.Foreach(e => e.RestoreMemebers());

            _weaponOnGroundFactory.Discard();
            _propFactory.Discard();
        }

        private enum FightState
        {
            Planning,
            Foresight,
            RealFight
        }
    }
}