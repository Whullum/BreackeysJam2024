using System;
using _Scripts.Extentions;
using _Scripts.Gameplay.SpotSystem;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private float _lerpMoveSpeed;

        public Spot CurrentSpot { get; private set; }

        public int Direction => transform.localScale.x.Sign();

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

        public void TryGoForward()
        {
            GoToSpot(CurrentSpot.IndexOnMap + Direction);
        }

        public void TurnAround()
        {
            transform.localScale = transform.localScale.WithX(transform.localScale.x * -1);
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
    }
}
