using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Extentions;
using _Scripts.Gameplay.Characters;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.SpotSystem
{
    /// <summary>
    /// We have 7 positions in an X axis (-3, -2, -1, 0 (initial position), 1, 2, 3).
    /// We can have one object or person (enemy or player) in each position.
    /// </summary>
    public class Spot : MonoBehaviour
    {
        private SpotMap _map;

        public List<SpotObject> _objects = new List<SpotObject>();

        public Vector2Int Coordinates { get; private set; }

        public void Init(SpotMap map, Vector2Int coordinates)
        {
            _map = map;
            Coordinates = coordinates;
        }
        
        public T GetObject<T>() where T : SpotObject
            => (T) _objects.FirstOrDefault(o => o is T);

        public bool IsOccupiedBy<T>() where T : SpotObject
            => _objects.Any(o => o is T);

        public Spot GetAdjacentSpot(Vector2Int delta) => _map.GetSpot(Coordinates + delta);

        public bool TryOccupy(SpotObject spotObject)
        {
            Type objectType = spotObject.GetType();
            if (_objects.Any(o => o.GetType() == objectType))
                return false;
            
            _objects.Add(spotObject);
            return true;
        }

        public void ForceLeave<T>() where T : SpotObject
        {
            SpotObject objectToLeave = _objects.FirstOrDefault(o => o is T);
            if (objectToLeave == null)
                return;
            
            objectToLeave.OnForceLeave();
            objectToLeave.LeaveCurrentSpot();
        }
        
        public void Leave(SpotObject spotObject)
        {
            _objects.TryRemove(spotObject);
        }
    
    }
}
