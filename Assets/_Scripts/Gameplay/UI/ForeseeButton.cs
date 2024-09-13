using _Scripts.Gameplay.Execution;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class ForeseeButton : MonoBehaviour
    {
        [Inject]
        private FightPlayer _fightPlayer;

        public void Foresee()
        {
            _fightPlayer.PlayFight();
        }
    }
}