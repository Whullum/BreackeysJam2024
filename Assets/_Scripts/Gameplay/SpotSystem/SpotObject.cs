﻿using System;
using _Scripts.Gameplay.Execution;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.SpotSystem
{
    public abstract class SpotObject : MonoBehaviour, IDiscardable, IRestorable
    {
        [SerializeField]
        private float _lerpMoveSpeed;
        
        private Vector2Int _homeSpot;

        public Spot CurrentSpot { get; private set; }

        public Vector2Int Coordinates => CurrentSpot.Coordinates;

        public bool IsInAir => Coordinates.y > 0;

        protected virtual bool ReturnToHomeSpotOnDiscard => true;
        
        [Inject]
        protected SpotMap SpotMap { get; private set; }

        public event Action MadeStep;

        private void FixedUpdate()
        {
            AlignToSpot();
        }
        
        private void AlignToSpot()
        {
            if (CurrentSpot == null)
                return;
            transform.position = Vector3.Lerp(transform.position, CurrentSpot.transform.position, Time.fixedDeltaTime * _lerpMoveSpeed);
        }
        
        public void AssignHomeSpot(Vector2Int homeSpot)
        {
            _homeSpot = homeSpot;
            ReturnToHomeSpot();
        }

        public void ReturnToHomeSpot()
        {
            GoToSpot(_homeSpot, true);
        }

        public void GoToSpot(Vector2Int coordinates, bool teleport = false)
        {
            Spot destination = SpotMap.GetSpot(coordinates);
            
            if (destination == null || destination == CurrentSpot)
                return;
            
            if ( ! destination.TryOccupy(this))
                return;

            LeaveCurrentSpot();
            CurrentSpot = destination;
            
            if (teleport)
            {
                transform.position = destination.transform.position;
            }
            else
            {
                MadeStep?.Invoke();
            }
        }

        public abstract void OnForceLeave();

        public void LeaveCurrentSpot()
        {
            CurrentSpot?.Leave(this);
            CurrentSpot = null;
        }

        public virtual void Discard()
        {
            if (ReturnToHomeSpotOnDiscard)
                LeaveCurrentSpot();
        }

        public virtual void Restore()
        {
            if (ReturnToHomeSpotOnDiscard)
                ReturnToHomeSpot();
        }
    }
}