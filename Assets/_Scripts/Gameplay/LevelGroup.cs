using System.Collections.Generic;
using System.Linq;
using _Scripts.Extentions;
using UnityEngine;

namespace _Scripts.Gameplay
{
    [CreateAssetMenu(menuName = "Level group")]
    public class LevelGroup : LevelSource
    {
        [SerializeField]
        private LevelData[] _levels;

        private List<LevelData> _pool = new List<LevelData>();
        
        public override LevelData Level
        {
            get
            {
                TryReplenishPool();
                return CollectionExtentions.TakeAwayRandomElement(ref _pool);
            }
        }

        public void TryReplenishPool()
        {
            if (_pool.Count > 0)
                return;
            _pool = _levels.ToList();
        }

    }
}