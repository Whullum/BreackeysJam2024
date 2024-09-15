using System;
using _Scripts.Gameplay.Characters;
using _Scripts.UI.Project;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Execution
{
    public class ComboSystem
    {
        private int _currentTurnMove;
        private int _previousTurnMove;
        
        
        public bool IsComboActive { get; private set; }
        
        public bool IsComboUpcoming => ! IsComboActive && _currentTurnMove > 0 || _previousTurnMove > 0;

        public int CurrentCombo { get; private set; }
        
        public event Action ComboStarted;
        public event Action ComboEnded;
        
        [Inject]
        private ForesightUsage _foresightUsage;

        [Inject]
        public ComboSystem(PlayerMarker player)
        {
            player.Attack.Punched += () => MarkTurnAsValuable(1);
            player.Attack.Kicked += () => MarkTurnAsValuable(2);
            player.Attack.UsedWeapon += () => MarkTurnAsValuable(3);
            player.Attack.Disarmed += () => MarkTurnAsValuable(4);
            player.Life.Dodged += () => MarkTurnAsValuable(5);
        }

        public void GetSyncFromCombo()
        {
            _foresightUsage.AddUses(CurrentCombo);
        }

        public void UpdateComboState()
        {
            IsComboActive = _currentTurnMove != 0 && _previousTurnMove != 0 &&  _currentTurnMove != _previousTurnMove;

            if (IsComboActive)
            {
                ComboStarted?.Invoke();
            }
            else
            {
                GetSyncFromCombo();
                ComboEnded?.Invoke();
            }

            Debug.Log(IsComboActive);
            CurrentCombo = IsComboActive ? (CurrentCombo + 1) : 0;
        }

        public void MarkTurnAsValuable(int move)
        {
            _currentTurnMove = move;
        }

        public void SwitchTurns()
        {
            _previousTurnMove = _currentTurnMove;
            _currentTurnMove = 0;
        }
    }
}