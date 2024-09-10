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
        private float _gridSize;
        
        [SerializeField]
        private int _spotsCount;
        
        [SerializeField]
        private GameObject _spotPrefab; 
        
        private readonly List<Spot> _spots = new List<Spot>();

        [Inject]
        private ContainerFactory ContainerFactory { get; set; }

        public Spot GetSpot(int index)
        {
            if (index < 0 || index >= _spots.Count)
                return null;
            return _spots[index];
        }

        private void Awake()
        {
            GenerateSpots();
        }

        private void GenerateSpots()
        {
            for (int i = 0; i < _spotsCount; i++)
            {
                Spot newSpot = ContainerFactory.Instantiate<Spot>(_spotPrefab, transform.position, transform);
                newSpot.Init(this, i);
                _spots.Add(newSpot);
            }

            RearrangeSpots();
        }

        private void RearrangeSpots()
        {
            float median = (float) (_spots.Count - 1) / 2;
            
            for (int i = 0; i < _spotsCount; i++)
            {
                float offset = i - median;
                _spots[i].transform.position = new Vector3(offset * _gridSize, 0, 0);
            }
        }
    }
}
