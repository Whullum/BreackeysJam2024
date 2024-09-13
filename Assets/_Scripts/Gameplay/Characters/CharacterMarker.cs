using _Scripts.Extentions;
using _Scripts.Gameplay.Execution;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMarker : MonoBehaviour
    {
        private IDiscardable[] _discardables;
        private IRestorable[] _restorables;
        private CharacterMovement _movement;
        private CharacterLife _life;
        private CharacterAttack attack;

        private IDiscardable[] Discardables => _discardables ??= GetComponentsInChildren<IDiscardable>();
        private IRestorable[] Restorables => _restorables ??= GetComponentsInChildren<IRestorable>();
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();
        public CharacterLife Life => _life ??= GetComponent<CharacterLife>(); 
        public CharacterAttack Attack => attack ??= GetComponent<CharacterAttack>();

        public void DiscardMemebers()
        {
            Discardables.Foreach(d => d.Discard());
        }

        public void RestoreMemebers()
        {
            Restorables.Foreach(r => r.Restore());
        }
    }
}