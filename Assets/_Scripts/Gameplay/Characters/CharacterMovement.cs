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
        
        private Spot _currentSpot;

        public int Direction => transform.localScale.x.Sign();
    
        [Inject]
        private SpotMap SpotMap { get; set; }

        [Inject]
        public void Construct(SpotMap sm)
        {
            this.SpotMap = sm;
        } 
    
        private void FixedUpdate()
        {
            AlignToSpot();
        }
    
        private void AlignToSpot()
        {
            if (_currentSpot == null)
                return;
            transform.position = Vector3.Lerp(transform.position, _currentSpot.transform.position, Time.fixedDeltaTime * _lerpMoveSpeed);
        }

        public void TryGoForward()
        {
            GoToSpot(_currentSpot.IndexOnMap + Direction);
        }

        public void TurnAround()
        {
            transform.localScale = transform.localScale.WithX(transform.localScale.x * -1);
        }
    
        public void GoToSpot(int index, bool teleport = false)
        {
            Spot destination = SpotMap.GetSpot(index);
            
            if (destination == null || destination == _currentSpot)
                return;
            
            if ( ! destination.TryOccupy(this))
                return;

            _currentSpot?.Leave();
            _currentSpot = destination;
            
            if (teleport)
            {
                transform.position = destination.transform.position;
            } 
        }
    }
}
