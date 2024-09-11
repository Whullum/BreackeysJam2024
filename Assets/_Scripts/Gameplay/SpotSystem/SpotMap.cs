using System.Collections.Generic;
using System.Linq;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.SpotSystem
{
    public class SpotMap : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _gridSize;
        
        [SerializeField]
        private Vector2Int _spotsCount;
        
        [SerializeField]
        private GameObject _spotPrefab; 
        
        private Spot[,] _spots;

        [Inject]
        private ContainerFactory ContainerFactory { get; set; }

        public Spot GetSpot(Vector2Int coordinates)
        {
            if (coordinates.x < 0 || coordinates.x >= _spots.GetLength(0) || coordinates.y < 0 ||
                coordinates.y >= _spots.GetLength(1))
                return null;
            
            return _spots[coordinates.x, coordinates.y];
        }

        private void Awake()
        {
            GenerateSpots();
        }

        private void GenerateSpots()
        {
            _spots = new Spot[_spotsCount.x, _spotsCount.y];
            
            for (int y = 0; y < _spotsCount.y; y++)
            {
                for (int x = 0; x < _spotsCount.x; x++)
                {
                    Spot newSpot = ContainerFactory.Instantiate<Spot>(_spotPrefab, transform.position, transform);
                    newSpot.Init(this, new Vector2Int(x, y));
                    _spots[x, y] = newSpot;
                }
            }

            RearrangeSpots();
        }

        private void RearrangeSpots()
        {
            float xMedian = (float) (_spots.GetLength(0) - 1) / 2;
            
            for (int y = 0; y < _spots.GetLength(1); y++)
            {
                for (int x = 0; x < _spots.GetLength(0); x++)
                {
                    float xOffset = x - xMedian;
                    _spots[x, y].transform.position = new Vector3(xOffset * _gridSize.x, y * _gridSize.y, 0);
                }
            }
        }
    }
}
