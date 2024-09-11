using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    public abstract class Move : ScriptableObject
    {
        [SerializeField]
        private string _name;
        
        [SerializeField] [TextArea(2, 10)]
        private string _description;
        
        [SerializeField]
        private Sprite _icon;

        public string Name => _name;

        public string Description => _description;

        public Sprite Icon => _icon;
        
        public abstract void Execute(PlayerMarker player);
    }
}