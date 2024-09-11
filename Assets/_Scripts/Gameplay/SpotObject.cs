using _Scripts.Gameplay.SpotSystem;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay
{
    public abstract class SpotObject : MonoBehaviour, IDiscardable, IRestorable
    {
        [SerializeField]
        private float _lerpMoveSpeed;
        
        private Vector2Int _homeSpot;

        public Spot CurrentSpot { get; set; }

        public Vector2Int Coordinates => CurrentSpot.Coordinates;
        
        [Inject]
        protected SpotMap SpotMap { get; private set; }

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
        }

        public abstract void OnForceLeave();

        public void LeaveCurrentSpot()
        {
            CurrentSpot?.Leave(this);
        }

        public virtual void Discard() => LeaveCurrentSpot();

        public virtual void Restore()
        {
            ReturnToHomeSpot();
        }
    }
}