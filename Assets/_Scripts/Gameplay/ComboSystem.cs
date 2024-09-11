using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public class ComboSystem
    {
        public bool IsCurrentTurnCombo { get; private set; }

        [Inject]
        public ComboSystem(PlayerMarker player)
        {
            player.Attack.Punched += MarkTurnAsCombo;
            player.Attack.Kicked += MarkTurnAsCombo;
            player.Attack.UsedWeapon += MarkTurnAsCombo;
        }

        public void MarkTurnAsCombo()
        {
            IsCurrentTurnCombo = true;
        }

        public bool TrySpendCombo()
        {
            if ( ! IsCurrentTurnCombo)
                return false;
            IsCurrentTurnCombo = false;
            return true;
        }
    }
}