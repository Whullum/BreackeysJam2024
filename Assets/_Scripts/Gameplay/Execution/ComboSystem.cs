using System;
using _Scripts.Gameplay.Characters;
using Zenject;

namespace _Scripts.Gameplay.Execution
{
    public class ComboSystem
    {
        private int _currentTurnMove;
        private int _previousTurnMove;
        
        public bool IsComboActive { get; private set; }

        public event Action ComboStarted;
        public event Action ComboEnded;

        [Inject]
        public ComboSystem(PlayerMarker player)
        {
            player.Attack.Punched += () => MarkTurnAsValuable(1);
            player.Attack.Kicked += () => MarkTurnAsValuable(2);
            player.Attack.UsedWeapon += () => MarkTurnAsValuable(3);
            player.Attack.Disarmed += () => MarkTurnAsValuable(4);
            player.Life.Dodged += () => MarkTurnAsValuable(5);
        }

        public void UpdateComboState()
        {
            IsComboActive = _currentTurnMove != 0 && _previousTurnMove != 0 &&  _currentTurnMove != _previousTurnMove;
            if (IsComboActive)
                ComboStarted?.Invoke();
            else
                ComboEnded?.Invoke();
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