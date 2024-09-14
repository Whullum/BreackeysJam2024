using UnityEngine;

namespace _Scripts.Gameplay
{
    public abstract class LevelSource : ScriptableObject
    {
        public abstract LevelData Level { get; }
    }
}