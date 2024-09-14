using _Scripts.Gameplay.Execution;
using _Scripts.UI.Project;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class FightButton : MonoBehaviour
    {
        [SerializeField]
        private bool _real;
        
        [Inject]
        private FightPlayer _fightPlayer;
        
        [Inject]
        private ForesightUsage _foresightUsage;

        public void Fight()
        {
            if (_real)
            {
                _fightPlayer.PlayFight(true);
            }
            else if (_foresightUsage.TryUse())
            {
                _fightPlayer.PlayFight(false);
            }
        }
    }
}