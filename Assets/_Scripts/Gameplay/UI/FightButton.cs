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

        [Inject]
        private ManaBarController _manaController;

        [Inject]
        private SoundManager _soundManager;

        private void OnEnable()
        {
            _manaController.SetManaBar(_foresightUsage.UsagePercentage);
        }

        public void Fight()
        {
            if (_real)
            {
                _fightPlayer.PlayFight(true);
                _soundManager.PlayButtonClickSFX(ButtonClickSFX.Fight);
            }
            else
            {
                if (_foresightUsage.TryUse())
                {
                    _fightPlayer.PlayFight(false);
                }
                _manaController.SetManaBar(_foresightUsage.UsagePercentage);

                _soundManager.PlayButtonClickSFX(ButtonClickSFX.Simulation);

            }

        }
    }
}