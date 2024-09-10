using _Scripts.Extentions;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMarker : MonoBehaviour
    {
        private IDiscardable[] _discardables;
        private IRestorable[] _restorables;
        private CharacterMovement _movement;
        private CharacterLife _life;
        private CharacterStrikes _strikes;

        public IDiscardable[] Discardables => _discardables ??= GetComponentsInChildren<IDiscardable>();
        public IRestorable[] Restorables => _restorables ??= GetComponentsInChildren<IRestorable>();
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();
        public CharacterLife Life => _life ??= GetComponent<CharacterLife>(); 
        public CharacterStrikes Strikes => _strikes ??= GetComponent<CharacterStrikes>();

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