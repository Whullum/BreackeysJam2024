using _Scripts.Gameplay.Turns;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class ForeseeButton : MonoBehaviour
    {
        [Inject]
        private FightPlayer FightPlayer { get; set; }

        public void Foresee()
        {
            FightPlayer.PlayFight();
        }
    }
}