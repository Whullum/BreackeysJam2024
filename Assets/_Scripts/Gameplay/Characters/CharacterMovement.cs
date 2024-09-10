using System;
using _Scripts.Extentions;
using _Scripts.Gameplay.SpotSystem;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMovement : MonoBehaviour, IDiscardable, IRestorable
    {
        [SerializeField]
        private float _lerpMoveSpeed;
        
        private int _homeSpot;

        public Spot CurrentSpot { get; private set; }

        public int Direction
        {
            get => transform.localScale.x.Sign();
            set
            {
                if (value == 0)
                    return;
                transform.localScale = transform.localScale.WithX(value.Sign());
            }
        }

        private CharacterLife _life;
        public CharacterLife Life => _life ??= GetComponent<CharacterLife>(); 
    
        [Inject]
        private SpotMap SpotMap { get; set; }

        [Inject]
        public void Construct(SpotMap sm)
        {
            this.SpotMap = sm;
        }

        private void Awake()
        {
            Life.Died += LeaveCurrentSpot;
        }

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

        public void TryGoForward() => GoToSpot(CurrentSpot.IndexOnMap + Direction);

        public void TryGoBackwards() => GoToSpot(CurrentSpot.IndexOnMap - Direction);

        public void TurnAround()
        {
            Direction = -Direction;
        }

        public void AssignHomeSpot(int homeSpot)
        {
            _homeSpot = homeSpot;
            ReturnToHomeSpot();
        }

        public void ReturnToHomeSpot()
        {
            GoToSpot(_homeSpot, true);
        }

        public void GoToSpot(int index, bool teleport = false)
        {
            Spot destination = SpotMap.GetSpot(index);
            
            if (destination == null || destination == CurrentSpot)
                return;
            
            if ( ! destination.TryOccupy(this))
                return;

            CurrentSpot?.Leave();
            CurrentSpot = destination;
            
            if (teleport)
            {
                transform.position = destination.transform.position;
            } 
        }

        private void LeaveCurrentSpot()
        {
            CurrentSpot.Leave();
        }

        public void Discard() => CurrentSpot.Leave();

        public void Restore()
        {
            ReturnToHomeSpot();
            Direction = 1;
        }
    }
}
