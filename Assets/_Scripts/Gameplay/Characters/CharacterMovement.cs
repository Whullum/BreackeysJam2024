using System;
using _Scripts.Extentions;
using _Scripts.Gameplay.SpotSystem;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMovement : SpotObject
    {
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
    

        private void Awake()
        {
            Life.Died += LeaveCurrentSpot;
        }

        public void TurnAround()
        {
            Direction = -Direction;
        }

        public void TryGoForward() => GoToSpot(CurrentSpot.IndexOnMap + Direction);

        public void TryGoBackwards() => GoToSpot(CurrentSpot.IndexOnMap - Direction);

        public override void OnForceLeave()
        {
            Life.Kill();
        }
    }
}
