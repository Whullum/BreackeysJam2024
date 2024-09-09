using _Scripts.Gameplay.Turns;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class ForeseeButton : MonoBehaviour
    {
        [Inject]
        private TurnsSystem TurnsSystem { get; set; }

        public void Foresee()
        {
            TurnsSystem.BuildTurnsSequence();
        }
    }
}